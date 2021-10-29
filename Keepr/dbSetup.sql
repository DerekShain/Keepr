CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

SELECT * from accounts;

CREATE TABLE IF NOT EXISTS keeps(
  id int NOT NULL primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name VARCHAR(255) NOT NULL COMMENT 'Keep Name',
  description VARCHAR(255) NOT NULL COMMENT 'Keep Description',
  img VARCHAR(255) NOT NULL COMMENT 'Keep Image',
  views int DEFAULT 0 COMMENT 'Keep Views',
  shares int DEFAULT 0 COMMENT 'Keep Shares',
  keeps int DEFAULT 0 COMMENT 'Keep Keeps',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name VARCHAR(255) NOT NULL COMMENT 'Vault Name',
  description VARCHAR(255) NOT NULL COMMENT 'Vault Description',
  isPrivate BOOLEAN DEFAULT 0 COMMENT 'Vault Is Private',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';