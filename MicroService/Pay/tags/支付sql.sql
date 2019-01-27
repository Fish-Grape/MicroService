
-- ==========================================《支持第三方表 begin》===============


DROP TABLE IF EXISTS pay_support;
CREATE TABLE
IF
	NOT EXISTS pay_support(
	id INT PRIMARY key auto_increment COMMENT '主键',
	platkey VARCHAR(30) COMMENT '平台key',
	pay_name VARCHAR(20) COMMENT '支付名称',
	pay_ico VARCHAR(50) COMMENT '支付图标',
	is_disable TINYINT COMMENT '是否禁用'
	)ENGINE = INNODB,
	charset = utf8,
	COMMENT '支持第三方表'; 


DROP TABLE IF EXISTS pay_account;
CREATE TABLE
IF
	NOT EXISTS pay_account(
	id INT PRIMARY key auto_increment COMMENT '主键',
	platkey VARCHAR(30) COMMENT '平台key',
	
	appid VARCHAR(30) COMMENT '关联商户id',
	public_key VARCHAR(2000) COMMENT '公钥',
	notify_url VARCHAR(50) COMMENT '通知地址',
	-- 支付宝
	return_url VARCHAR(50) COMMENT '返回地址',
	private_key VARCHAR(2000) COMMENT '私钥',
	gate_way_url VARCHAR(50) COMMENT '路由地址',
	
	-- 微信
	mchid VARCHAR(30) COMMENT '机器id',
	pay_key VARCHAR(50) COMMENT 'key',
	appsecret VARCHAR(50) COMMENT '秘钥',
	sslcert_path VARCHAR(30) COMMENT '证书地址',
	sslcert_password VARCHAR(30) COMMENT '证书密码'
	
	)ENGINE = INNODB,
	charset = utf8,
	COMMENT '支持第三方表'; 






-- ==========================================《支持第三方表 begin》===============

