CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

SELECT * from accounts;

CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name VARCHAR(255)  COMMENT 'Keep Name',
  description VARCHAR(255)  COMMENT 'Keep Description',
  img VARCHAR(255) COMMENT 'Keep Image',
  views int  COMMENT 'Keep Views',
  shares int  COMMENT 'Keep Shares',
  keeps int  COMMENT 'Keep Keeps',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

SELECT * from keeps;

INSERT INTO keeps( 
  name,
  description,
  img,
  views,
  shares,
  keeps,
  creatorId
)VALUES(
  "test",
  "test",
  "test",
  1,
  1,
  1,
  "6140ce20fdb86736f610885d"
);

CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name VARCHAR(255) NOT NULL COMMENT 'Vault Name',
  description VARCHAR(255) NOT NULL COMMENT 'Vault Description',
  isPrivate TINYINT DEFAULT 0 COMMENT 'Vault Is Private',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS profiles (
  id int NOT NULL primary key AUTO_INCREMENT  COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name VARCHAR(255) NOT NULL COMMENT 'Profile Name',
  email VARCHAR(255) NOT NULL COMMENT 'Profile Email',
  picture VARCHAR(255) NOT NULL COMMENT 'Profile Picture',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

