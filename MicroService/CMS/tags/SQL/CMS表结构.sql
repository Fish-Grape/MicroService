-- ==========================================《导航大类 begin》===============

DROP TABLE IF EXISTS cms_menu_classfy;

CREATE TABLE
IF
	NOT EXISTS cms_menu_classfy (
	id INT NOT NULL auto_increment COMMENT '主键id',
	appid VARCHAR ( 50 ) NOT NULL COMMENT '应用key',
	class_name VARCHAR ( 50 ) NOT NULL COMMENT '导航名',
	classfy VARCHAR(50) COMMENT '分类',
	PRIMARY KEY ( id, appid) 
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '菜单大类';
	
	
TRUNCATE TABLE cms_menu_classfy;
INSERT INTO cms_menu_classfy ( appid, class_name, classfy )
VALUES
	( 'AuthPlat', 'CMS系统', 'CMS' );
	
-- ==========================================《导航大类 end》===============






-- ==========================================《菜单导航 begin》===============
DROP TABLE IF EXISTS cms_menu;

CREATE TABLE
IF
	NOT EXISTS cms_menu (
	id INT NOT NULL auto_increment COMMENT '主键id',
	appid VARCHAR ( 50 ) NOT NULL COMMENT '应用key',
	-- CMS_MenuCode VARCHAR ( 50 ) NOT NULL UNIQUE COMMENT '导航code',
	menu_name VARCHAR ( 50 ) NOT NULL COMMENT '导航名',
	navigate_url VARCHAR ( 255 ) NOT NULL COMMENT '请求地址',
	target INT DEFAULT 0 COMMENT '目标',
	sort INT NOT NULL COMMENT '排序',
	pid int COMMENT '父级id',
	classfy VARCHAR(50) COMMENT '分类',
	PRIMARY KEY ( id, appid) 
	) ENGINE = INNODB,
	charset = utf8,
	COMMENT '导航菜单';
	
	
	
TRUNCATE TABLE cms_menu;

INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'产品认证',-- menu_name
	'',-- navigate_url
	'0',-- target
	0,-- sort
	'0',-- pid
	'' -- css_class
	);
	INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
	VALUES
	(
	'123456',-- appid
	'Banner管理',-- menu_name
	'/FlatSet/BannerManage',-- navigate_url
	'0',-- target
	1,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	) ,
	(
	'123456',-- appid
	'公告管理',-- menu_name
	'/FlatSet/NoticeManage',-- navigate_url
	'0',-- target
	2,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	);




INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'体系认证',-- menu_name
	'',-- navigate_url
	'0',-- target
	0,-- sort
	'0',-- pid
	'' -- css_class
	);
	INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'产品认证管理',-- menu_name
	'/ProManage/ProQulifyMng',-- navigate_url
	'0',-- target
	1,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	) ,
	(
	'123456',-- appid
	'体系认证管理',-- menu_name
	'/ProManage/SerQulifyMng',-- navigate_url
	'0',-- target
	2,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	);




INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'企业管理',-- menu_name
	'',-- navigate_url
	'0',-- target
	0,-- sort
	'0',-- pid
	'' -- css_class
	) ;
	INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'产品认证订单',-- menu_name
	'/OrderManage/ProQulifyOrder',-- navigate_url
	'0',-- target
	1,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	) ,
	(
	'123456',-- appid
	'体系认证订单',-- menu_name
	'/OrderManage/SerQulifyOrder',-- navigate_url
	'0',-- target
	2,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	);




INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'管理软件',-- menu_name
	'',-- navigate_url
	'0',-- target
	0,-- sort
	'0',-- pid
	'' -- css_class
	);
	
	
	
	
	
INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'只能制造',-- menu_name
	'',-- navigate_url
	'0',-- target
	0,-- sort
	'0',-- pid
	'' -- css_class
	) ;
	INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'产品列表',-- menu_name
	'/UserManage/Productlist',-- navigate_url
	'0',-- target
	1,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	);
	
	
	
	
	
INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'学习培训',-- menu_name
	'',-- navigate_url
	'0',-- target
	0,-- sort
	'0',-- pid
	'' -- css_class
	);
	INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'账号管理',-- menu_name
	'/AppSet/AccountMng',-- navigate_url
	'0',-- target
	1,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	) ,
	(
	'123456',-- appid
	'权限设置',-- menu_name
	'/AppSet/RightSet',-- navigate_url
	'0',-- target
	2,-- sort
	LAST_INSERT_ID(),-- pid
	'' -- css_class
	);
	
	
INSERT INTO cms_menu ( appid, menu_name, navigate_url, target, sort, pid, classfy )
VALUES
	(
	'123456',-- appid
	'关于艾磊',-- menu_name
	'',-- navigate_url
	'0',-- target
	0,-- sort
	'0',-- pid
	'' -- css_class
	);
	
	UPDATE cms_menu SET classfy='CMS',appid='AuthPlat';
-- ==========================================《菜单导航 end》===============




-- 
-- -- ==========================================《公告 begin》===============
-- CREATE TABLE
-- IF
-- 	NOT EXISTS cms_notice (
-- 	id INT NOT NULL auto_increment COMMENT '主键id',
-- 	appid VARCHAR ( 50 ) NOT NULL COMMENT '应用key',
-- 	title varchar(100) comment '公告标题',
-- 	detail varchar(2000) comment '详细内容',
-- 	isdisable int comment '是否禁用',
-- 	PRIMARY KEY ( id, appid) 
-- 	) ENGINE = myisam,
-- 	charset = utf8,
-- 	COMMENT '公告';
-- 	
-- 	
-- -- ==========================================《公告 end》===============
-- 
-- 
