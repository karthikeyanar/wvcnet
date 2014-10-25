-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Oct 25, 2014 at 07:02 AM
-- Server version: 5.6.20-log
-- PHP Version: 5.4.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `wvc`
--
CREATE DATABASE IF NOT EXISTS `wvc` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `wvc`;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroles`
--

CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(166) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `aspnetroles`
--

INSERT INTO `aspnetroles` (`Id`, `Name`) VALUES
('d0a94f7e-433d-4c87-a527-e342e9a618b7', 'Admin');

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserclaims`
--

CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` varchar(256) DEFAULT NULL,
  `ClaimValue` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserlogins`
--

CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserroles`
--

CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_UserId` (`UserId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `aspnetuserroles`
--

INSERT INTO `aspnetuserroles` (`UserId`, `RoleId`) VALUES
('4868b16e-7ca7-4735-8bf5-4a85be66208d', 'd0a94f7e-433d-4c87-a527-e342e9a618b7');

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusers`
--

CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(128) NOT NULL,
  `UserName` varchar(166) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` varchar(256) DEFAULT NULL,
  `SecurityStamp` varchar(256) DEFAULT NULL,
  `PhoneNumber` varchar(256) DEFAULT NULL,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `aspnetusers`
--

INSERT INTO `aspnetusers` (`Id`, `UserName`, `Email`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEndDateUtc`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('4868b16e-7ca7-4735-8bf5-4a85be66208d', 'admin@wvc.com', 'admin@wvc.com', b'1', 'ACZXhxLp0krNQwtSXA2caylIdyKgrPSl0G9o2puQVmGGvKaBTG5XETBmHsOrrnaehQ==', 'caf820ba-d33b-4ab6-b373-2a42fe45adb3', NULL, b'0', b'0', NULL, b'0', 0);

-- --------------------------------------------------------

--
-- Table structure for table `district`
--

CREATE TABLE IF NOT EXISTS `district` (
  `district_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`district_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `district`
--

INSERT INTO `district` (`district_id`, `name`) VALUES
(1, 'dis1'),
(2, 'dis2');

-- --------------------------------------------------------

--
-- Table structure for table `division`
--

CREATE TABLE IF NOT EXISTS `division` (
  `division_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`division_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `division`
--

INSERT INTO `division` (`division_id`, `name`) VALUES
(1, 'div1'),
(2, 'div2');

-- --------------------------------------------------------

--
-- Table structure for table `range`
--

CREATE TABLE IF NOT EXISTS `range` (
  `range_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`range_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `range`
--

INSERT INTO `range` (`range_id`, `name`) VALUES
(1, 'ran1'),
(2, 'ran2');

-- --------------------------------------------------------

--
-- Table structure for table `taluk`
--

CREATE TABLE IF NOT EXISTS `taluk` (
  `taluk_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`taluk_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `taluk`
--

INSERT INTO `taluk` (`taluk_id`, `name`) VALUES
(1, 'tal1'),
(2, 'tal2');

-- --------------------------------------------------------

--
-- Table structure for table `village`
--

CREATE TABLE IF NOT EXISTS `village` (
  `village_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`village_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `village`
--

INSERT INTO `village` (`village_id`, `name`) VALUES
(1, 'vil1'),
(2, 'vil2');

-- --------------------------------------------------------

--
-- Table structure for table `wvc_user`
--

CREATE TABLE IF NOT EXISTS `wvc_user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `aspnetuser_id` varchar(128) NOT NULL,
  `first_name` varchar(30) DEFAULT NULL,
  `last_name` varchar(30) DEFAULT NULL,
  `is_active` bit(1) DEFAULT NULL,
  `created_by` int(11) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `last_updated_by` int(11) DEFAULT NULL,
  `last_updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `wvc_user`
--

INSERT INTO `wvc_user` (`user_id`, `aspnetuser_id`, `first_name`, `last_name`, `is_active`, `created_by`, `created_date`, `last_updated_by`, `last_updated_date`) VALUES
(1, '4868b16e-7ca7-4735-8bf5-4a85be66208d', 'admin', NULL, b'1', NULL, '2014-09-04 13:14:29', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `wvc_wood_volume`
--

CREATE TABLE IF NOT EXISTS `wvc_wood_volume` (
  `wood_volume_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `name` varchar(50) NOT NULL,
  `description` varchar(200) DEFAULT NULL,
  `division_id` int(11) DEFAULT NULL,
  `district_id` int(11) DEFAULT NULL,
  `range_id` int(11) DEFAULT NULL,
  `taluk_id` int(11) DEFAULT NULL,
  `village_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`wood_volume_id`),
  KEY `wvc_wood_volume_fk_user` (`user_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `wvc_wood_volume`
--

INSERT INTO `wvc_wood_volume` (`wood_volume_id`, `user_id`, `name`, `description`, `division_id`, `district_id`, `range_id`, `taluk_id`, `village_id`) VALUES
(1, NULL, 'test', 'test22', 2, 2, 2, 2, 2),
(2, NULL, 'test2', NULL, NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `wvc_wood_volum_item`
--

CREATE TABLE IF NOT EXISTS `wvc_wood_volum_item` (
  `wood_volume_item_id` int(11) NOT NULL AUTO_INCREMENT,
  `wood_volume_id` int(11) NOT NULL,
  `description` varchar(200) NOT NULL,
  `length` float(19,4) DEFAULT NULL,
  `girth` float(19,5) DEFAULT NULL,
  `volume` float(19,4) DEFAULT NULL,
  `co_efficient` float(19,4) DEFAULT NULL,
  `final_volume` float(19,4) DEFAULT NULL,
  PRIMARY KEY (`wood_volume_item_id`),
  KEY `wvc_wood_volum_item_fk_wood_volume` (`wood_volume_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `wvc_wood_volume`
--
ALTER TABLE `wvc_wood_volume`
  ADD CONSTRAINT `wvc_wood_volume_fk_user` FOREIGN KEY (`user_id`) REFERENCES `wvc_user` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `wvc_wood_volum_item`
--
ALTER TABLE `wvc_wood_volum_item`
  ADD CONSTRAINT `wvc_wood_volum_item_fk_wood_volume` FOREIGN KEY (`wood_volume_id`) REFERENCES `wvc_wood_volume` (`wood_volume_id`) ON DELETE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
