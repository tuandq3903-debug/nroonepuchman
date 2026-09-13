-- Boss configuration is intentionally separate from mob_template.
-- Remove the temporary columns from the earlier admin-panel experiment.
SET @drop_armor = IF(
    EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'mob_template' AND COLUMN_NAME = 'armor'),
    'ALTER TABLE mob_template DROP COLUMN armor', 'SELECT 1');
PREPARE stmt_drop_armor FROM @drop_armor;
EXECUTE stmt_drop_armor;
DEALLOCATE PREPARE stmt_drop_armor;

SET @drop_damage = IF(
    EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'mob_template' AND COLUMN_NAME = 'damage'),
    'ALTER TABLE mob_template DROP COLUMN damage', 'SELECT 1');
PREPARE stmt_drop_damage FROM @drop_damage;
EXECUTE stmt_drop_damage;
DEALLOCATE PREPARE stmt_drop_damage;

CREATE TABLE IF NOT EXISTS boss_stat_config (
    boss_id INT PRIMARY KEY,
    boss_name VARCHAR(255) NOT NULL DEFAULT '',
    hp BIGINT NOT NULL DEFAULT 0,
    damage BIGINT NOT NULL DEFAULT 0,
    armor BIGINT NOT NULL DEFAULT 0,
    enabled TINYINT(1) NOT NULL DEFAULT 1,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

SET @add_boss_name = IF(
    EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'boss_stat_config')
    AND NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'boss_stat_config' AND COLUMN_NAME = 'boss_name'),
    'ALTER TABLE boss_stat_config ADD COLUMN boss_name VARCHAR(255) NOT NULL DEFAULT '''' AFTER boss_id',
    'SELECT 1');
PREPARE stmt_add_boss_name FROM @add_boss_name;
EXECUTE stmt_add_boss_name;
DEALLOCATE PREPARE stmt_add_boss_name;

CREATE TABLE IF NOT EXISTS boss_drop_config (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    boss_id INT NOT NULL,
    item_id INT NOT NULL,
    chance INT NOT NULL DEFAULT 0 COMMENT '10000 = 100%',
    min_quantity INT NOT NULL DEFAULT 1,
    max_quantity INT NOT NULL DEFAULT 1,
    enabled TINYINT(1) NOT NULL DEFAULT 1,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_boss_drop_config_boss (boss_id),
    CONSTRAINT chk_boss_drop_chance CHECK (chance BETWEEN 0 AND 10000),
    CONSTRAINT chk_boss_drop_quantity CHECK (min_quantity > 0 AND max_quantity >= min_quantity)
);

-- Example: BossID.THAN_HUY_DIET = -239, item 555, 2.5% chance.
-- INSERT INTO boss_stat_config (boss_id, hp, damage, armor) VALUES (-239, 1000000000, 500000, 100000);
-- INSERT INTO boss_drop_config (boss_id, item_id, chance, min_quantity, max_quantity)
-- VALUES (-239, 555, 250, 1, 1);
