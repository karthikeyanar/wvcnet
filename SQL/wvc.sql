# SQL Manager Lite for MySQL 5.4.3.43929
# ---------------------------------------
# Host     : localhost
# Port     : 3306
# Database : wvc


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES latin1 */;

SET FOREIGN_KEY_CHECKS=0;

DROP DATABASE IF EXISTS `wvc`;

CREATE DATABASE `wvc`
    CHARACTER SET 'utf8'
    COLLATE 'utf8_general_ci';

USE `wvc`;

#
# Dropping database objects
#

DROP TABLE IF EXISTS `wvc_wood_volum_item`;
DROP TABLE IF EXISTS `wvc_wood_volume`;
DROP TABLE IF EXISTS `wvc_voucher_users`;
DROP TABLE IF EXISTS `wvc_voucher_type`;
DROP TABLE IF EXISTS `wvc_voucher_period`;
DROP TABLE IF EXISTS `wvc_voucher_inspection`;
DROP TABLE IF EXISTS `wvc_voucher_detail`;
DROP TABLE IF EXISTS `wvc_voucher`;
DROP TABLE IF EXISTS `wvc_user`;
DROP TABLE IF EXISTS `wvc_account_type`;
DROP TABLE IF EXISTS `village`;
DROP TABLE IF EXISTS `taluk`;
DROP TABLE IF EXISTS `range`;
DROP TABLE IF EXISTS `division`;
DROP TABLE IF EXISTS `district`;
DROP TABLE IF EXISTS `aspnetuserroles`;
DROP TABLE IF EXISTS `aspnetuserlogins`;
DROP TABLE IF EXISTS `aspnetuserclaims`;
DROP TABLE IF EXISTS `aspnetusers`;
DROP TABLE IF EXISTS `aspnetroles`;

#
# Structure for the `aspnetroles` table : 
#

CREATE TABLE `aspnetroles` (
  `Id` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  `Name` VARCHAR(166) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`Id`) COMMENT '',
  UNIQUE INDEX `RoleNameIndex` USING BTREE (`Name`) COMMENT ''
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `aspnetusers` table : 
#

CREATE TABLE `aspnetusers` (
  `Id` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  `UserName` VARCHAR(166) COLLATE utf8_general_ci NOT NULL,
  `Email` VARCHAR(256) COLLATE utf8_general_ci DEFAULT NULL,
  `EmailConfirmed` BIT(1) NOT NULL,
  `PasswordHash` VARCHAR(256) COLLATE utf8_general_ci DEFAULT NULL,
  `SecurityStamp` VARCHAR(256) COLLATE utf8_general_ci DEFAULT NULL,
  `PhoneNumber` VARCHAR(256) COLLATE utf8_general_ci DEFAULT NULL,
  `PhoneNumberConfirmed` BIT(1) NOT NULL,
  `TwoFactorEnabled` BIT(1) NOT NULL,
  `LockoutEndDateUtc` DATETIME DEFAULT NULL,
  `LockoutEnabled` BIT(1) NOT NULL,
  `AccessFailedCount` INTEGER(11) NOT NULL,
  PRIMARY KEY USING BTREE (`Id`) COMMENT '',
  UNIQUE INDEX `UserNameIndex` USING BTREE (`UserName`) COMMENT ''
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `aspnetuserclaims` table : 
#

CREATE TABLE `aspnetuserclaims` (
  `Id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  `ClaimType` VARCHAR(256) COLLATE utf8_general_ci DEFAULT NULL,
  `ClaimValue` VARCHAR(256) COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY USING BTREE (`Id`) COMMENT '',
  UNIQUE INDEX `Id` USING BTREE (`Id`) COMMENT '',
   INDEX `IX_AspNetUserClaims_UserId` USING BTREE (`UserId`) COMMENT '',
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE=InnoDB
AUTO_INCREMENT=1 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `aspnetuserlogins` table : 
#

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  `ProviderKey` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  `UserId` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`LoginProvider`, `ProviderKey`, `UserId`) COMMENT '',
   INDEX `IX_AspNetUserLogins_UserId` USING BTREE (`UserId`) COMMENT '',
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `aspnetuserroles` table : 
#

CREATE TABLE `aspnetuserroles` (
  `UserId` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  `RoleId` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`UserId`, `RoleId`) COMMENT '',
   INDEX `IX_AspNetUserRoles_UserId` USING BTREE (`UserId`) COMMENT '',
   INDEX `IX_AspNetUserRoles_RoleId` USING BTREE (`RoleId`) COMMENT '',
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `district` table : 
#

CREATE TABLE `district` (
  `district_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`district_id`) COMMENT '',
  UNIQUE INDEX `name` USING BTREE (`name`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=3 AVG_ROW_LENGTH=8192 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `division` table : 
#

CREATE TABLE `division` (
  `division_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`division_id`) COMMENT '',
  UNIQUE INDEX `name` USING BTREE (`name`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=3 AVG_ROW_LENGTH=8192 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `range` table : 
#

CREATE TABLE `range` (
  `range_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`range_id`) COMMENT '',
  UNIQUE INDEX `name` USING BTREE (`name`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=3 AVG_ROW_LENGTH=8192 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `taluk` table : 
#

CREATE TABLE `taluk` (
  `taluk_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`taluk_id`) COMMENT '',
  UNIQUE INDEX `name` USING BTREE (`name`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=3 AVG_ROW_LENGTH=8192 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `village` table : 
#

CREATE TABLE `village` (
  `village_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`village_id`) COMMENT '',
  UNIQUE INDEX `name` USING BTREE (`name`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=3 AVG_ROW_LENGTH=8192 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_account_type` table : 
#

CREATE TABLE `wvc_account_type` (
  `account_type_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`account_type_id`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=1 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_user` table : 
#

CREATE TABLE `wvc_user` (
  `user_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `aspnetuser_id` VARCHAR(128) COLLATE utf8_general_ci NOT NULL,
  `first_name` VARCHAR(30) COLLATE utf8_general_ci DEFAULT NULL,
  `last_name` VARCHAR(30) COLLATE utf8_general_ci DEFAULT NULL,
  `is_active` BIT(1) DEFAULT NULL,
  `created_by` INTEGER(11) DEFAULT NULL,
  `created_date` DATETIME DEFAULT NULL,
  `last_updated_by` INTEGER(11) DEFAULT NULL,
  `last_updated_date` DATETIME DEFAULT NULL,
  PRIMARY KEY USING BTREE (`user_id`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=2 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_voucher` table : 
#

CREATE TABLE `wvc_voucher` (
  `voucher_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `voucher_type_id` INTEGER(11) NOT NULL,
  `voucher_no` INTEGER(11) NOT NULL,
  `voucher_date` DATE NOT NULL,
  `name` VARCHAR(50) COLLATE utf8_general_ci NOT NULL,
  `sonof` VARCHAR(50) COLLATE utf8_general_ci DEFAULT NULL,
  `address` VARCHAR(1000) COLLATE utf8_general_ci NOT NULL,
  `place` VARCHAR(100) COLLATE utf8_general_ci DEFAULT NULL,
  `description` VARCHAR(1000) COLLATE utf8_general_ci DEFAULT NULL,
  `additional_description` VARCHAR(1000) COLLATE utf8_general_ci DEFAULT NULL,
  `quantity` DECIMAL(19,4) NOT NULL,
  `quantity_type` VARCHAR(5) COLLATE utf8_general_ci DEFAULT NULL,
  `rate` DECIMAL(19,4) NOT NULL,
  `per` INTEGER(11) NOT NULL,
  `per_type` VARCHAR(5) COLLATE utf8_general_ci NOT NULL,
  `amount` DECIMAL(19,4) NOT NULL,
  `wso_no` INTEGER(11) NOT NULL,
  `wso_no_year` VARCHAR(20) COLLATE utf8_general_ci NOT NULL,
  `account_type_id` INTEGER(11) NOT NULL,
  `district_id` INTEGER(11) DEFAULT NULL,
  `range_id` INTEGER(11) DEFAULT NULL,
  `agreement_date` DATE DEFAULT NULL,
  `book_no` VARCHAR(20) COLLATE utf8_general_ci DEFAULT NULL,
  `page_no` VARCHAR(20) COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY USING BTREE (`voucher_id`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=1 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_voucher_detail` table : 
#

CREATE TABLE `wvc_voucher_detail` (
  `voucher_detail_id` VARCHAR(200) COLLATE utf8_general_ci NOT NULL,
  `voucher_id` INTEGER(11) NOT NULL,
  `description` VARCHAR(1000) COLLATE utf8_general_ci NOT NULL,
  `detail_type_value` DECIMAL(19,4) DEFAULT NULL,
  `detail_type` VARCHAR(5) COLLATE utf8_general_ci DEFAULT NULL,
  `detail_type_days` DECIMAL(19,4) DEFAULT NULL,
  `detail_type_days_mode` VARCHAR(5) COLLATE utf8_general_ci DEFAULT NULL,
  `detail_type_calc` DECIMAL(19,4) DEFAULT NULL,
  `detail_place_value` DECIMAL(19,4) DEFAULT NULL,
  `detail_place_type` VARCHAR(5) COLLATE utf8_general_ci DEFAULT NULL,
  `detail_place_days` DECIMAL(19,4) DEFAULT NULL,
  `detail_place_days_mode` VARCHAR(5) COLLATE utf8_general_ci DEFAULT NULL,
  `amount` DECIMAL(19,4) DEFAULT NULL,
  PRIMARY KEY USING BTREE (`voucher_detail_id`) COMMENT '',
   INDEX `voucher_id` USING BTREE (`voucher_id`) COMMENT '',
  CONSTRAINT `wvc_voucher_detail_voucher` FOREIGN KEY (`voucher_id`) REFERENCES `wvc_voucher` (`voucher_id`) ON DELETE CASCADE
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_voucher_inspection` table : 
#

CREATE TABLE `wvc_voucher_inspection` (
  `voucher_inspection_id` VARCHAR(200) COLLATE utf8_general_ci NOT NULL,
  `voucher_id` INTEGER(11) NOT NULL,
  `inspection_date` DATE NOT NULL,
  PRIMARY KEY USING BTREE (`voucher_inspection_id`) COMMENT '',
  UNIQUE INDEX `voucher_inspection_id` USING BTREE (`voucher_inspection_id`) COMMENT '',
   INDEX `voucher_id` USING BTREE (`voucher_id`) COMMENT '',
  CONSTRAINT `wvc_voucher_inspection_voucher` FOREIGN KEY (`voucher_id`) REFERENCES `wvc_voucher` (`voucher_id`) ON DELETE CASCADE
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_voucher_period` table : 
#

CREATE TABLE `wvc_voucher_period` (
  `voucher_period_id` VARCHAR(200) COLLATE utf8_general_ci NOT NULL,
  `voucher_id` INTEGER(11) DEFAULT NULL,
  `start_date` DATE DEFAULT NULL,
  `end_date` DATE DEFAULT NULL,
  PRIMARY KEY USING BTREE (`voucher_period_id`) COMMENT '',
  UNIQUE INDEX `voucher_period_id` USING BTREE (`voucher_period_id`) COMMENT '',
   INDEX `voucher_id` USING BTREE (`voucher_id`) COMMENT '',
  CONSTRAINT `wvc_voucher_period_voucher` FOREIGN KEY (`voucher_id`) REFERENCES `wvc_voucher` (`voucher_id`) ON DELETE CASCADE
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_voucher_type` table : 
#

CREATE TABLE `wvc_voucher_type` (
  `voucher_type_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY USING BTREE (`voucher_type_id`) COMMENT ''
)ENGINE=InnoDB
AUTO_INCREMENT=1 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_voucher_users` table : 
#

CREATE TABLE `wvc_voucher_users` (
  `voucher_user_id` VARCHAR(200) COLLATE utf8_general_ci NOT NULL,
  `voucher_id` INTEGER(11) NOT NULL,
  `name` VARCHAR(50) COLLATE utf8_general_ci DEFAULT NULL,
  `sonof` VARCHAR(50) COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY USING BTREE (`voucher_user_id`) COMMENT ''
)ENGINE=InnoDB
CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_wood_volume` table : 
#

CREATE TABLE `wvc_wood_volume` (
  `wood_volume_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `user_id` INTEGER(11) DEFAULT NULL,
  `name` VARCHAR(50) COLLATE utf8_general_ci NOT NULL,
  `description` VARCHAR(200) COLLATE utf8_general_ci DEFAULT NULL,
  `division_id` INTEGER(11) DEFAULT NULL,
  `district_id` INTEGER(11) DEFAULT NULL,
  `range_id` INTEGER(11) DEFAULT NULL,
  `taluk_id` INTEGER(11) DEFAULT NULL,
  `village_id` INTEGER(11) DEFAULT NULL,
  PRIMARY KEY USING BTREE (`wood_volume_id`) COMMENT '',
   INDEX `wvc_wood_volume_fk_user` USING BTREE (`user_id`) COMMENT '',
  CONSTRAINT `wvc_wood_volume_fk_user` FOREIGN KEY (`user_id`) REFERENCES `wvc_user` (`user_id`) ON DELETE CASCADE
)ENGINE=InnoDB
AUTO_INCREMENT=2 AVG_ROW_LENGTH=8192 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Structure for the `wvc_wood_volum_item` table : 
#

CREATE TABLE `wvc_wood_volum_item` (
  `wood_volume_item_id` INTEGER(11) NOT NULL AUTO_INCREMENT,
  `wood_volume_id` INTEGER(11) NOT NULL,
  `description` VARCHAR(200) COLLATE utf8_general_ci NOT NULL,
  `length` DECIMAL(19,4) DEFAULT NULL,
  `girth` DECIMAL(19,4) DEFAULT NULL,
  `volume` DECIMAL(19,4) DEFAULT NULL,
  `co_efficient` DECIMAL(19,4) DEFAULT NULL,
  `final_volume` DECIMAL(19,4) DEFAULT NULL,
  PRIMARY KEY USING BTREE (`wood_volume_item_id`) COMMENT '',
   INDEX `wvc_wood_volum_item_fk_wood_volume` USING BTREE (`wood_volume_id`) COMMENT '',
  CONSTRAINT `wvc_wood_volum_item_fk_wood_volume` FOREIGN KEY (`wood_volume_id`) REFERENCES `wvc_wood_volume` (`wood_volume_id`) ON DELETE CASCADE
)ENGINE=InnoDB
AUTO_INCREMENT=1 CHARACTER SET 'utf8' COLLATE 'utf8_general_ci'
COMMENT=''
;

#
# Data for the `aspnetroles` table  (LIMIT -498,500)
#

INSERT INTO `aspnetroles` (`Id`, `Name`) VALUES

  ('d0a94f7e-433d-4c87-a527-e342e9a618b7','Admin');
COMMIT;

#
# Data for the `aspnetusers` table  (LIMIT -498,500)
#

INSERT INTO `aspnetusers` (`Id`, `UserName`, `Email`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEndDateUtc`, `LockoutEnabled`, `AccessFailedCount`) VALUES

  ('4868b16e-7ca7-4735-8bf5-4a85be66208d','admin@wvc.com','admin@wvc.com',1,'ACZXhxLp0krNQwtSXA2caylIdyKgrPSl0G9o2puQVmGGvKaBTG5XETBmHsOrrnaehQ==','caf820ba-d33b-4ab6-b373-2a42fe45adb3',NULL,0,0,NULL,0,0);
COMMIT;

#
# Data for the `aspnetuserroles` table  (LIMIT -498,500)
#

INSERT INTO `aspnetuserroles` (`UserId`, `RoleId`) VALUES

  ('4868b16e-7ca7-4735-8bf5-4a85be66208d','d0a94f7e-433d-4c87-a527-e342e9a618b7');
COMMIT;

#
# Data for the `district` table  (LIMIT -497,500)
#

INSERT INTO `district` (`district_id`, `name`) VALUES

  (1,'dis1'),
  (2,'dis2');
COMMIT;

#
# Data for the `division` table  (LIMIT -497,500)
#

INSERT INTO `division` (`division_id`, `name`) VALUES

  (1,'div1'),
  (2,'div2');
COMMIT;

#
# Data for the `range` table  (LIMIT -497,500)
#

INSERT INTO `range` (`range_id`, `name`) VALUES

  (1,'ran1'),
  (2,'ran2');
COMMIT;

#
# Data for the `taluk` table  (LIMIT -497,500)
#

INSERT INTO `taluk` (`taluk_id`, `name`) VALUES

  (1,'tal1'),
  (2,'tal2');
COMMIT;

#
# Data for the `village` table  (LIMIT -497,500)
#

INSERT INTO `village` (`village_id`, `name`) VALUES

  (1,'vil1'),
  (2,'vil2');
COMMIT;

#
# Data for the `wvc_user` table  (LIMIT -498,500)
#

INSERT INTO `wvc_user` (`user_id`, `aspnetuser_id`, `first_name`, `last_name`, `is_active`, `created_by`, `created_date`, `last_updated_by`, `last_updated_date`) VALUES

  (1,'4868b16e-7ca7-4735-8bf5-4a85be66208d','admin',NULL,1,NULL,'2014-09-04 13:14:29',NULL,NULL);
COMMIT;

#
# Data for the `wvc_wood_volume` table  (LIMIT -498,500)
#

INSERT INTO `wvc_wood_volume` (`wood_volume_id`, `user_id`, `name`, `description`, `division_id`, `district_id`, `range_id`, `taluk_id`, `village_id`) VALUES

  (1,NULL,'test','test22',2,2,2,2,2);
COMMIT;



/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;