-- ==========================================《订单表 begin》===============

DROP TABLE IF EXISTS orders;
CREATE TABLE
IF
	NOT EXISTS orders (
	id VARCHAR ( 30 ) COMMENT '主键id',
	platkey VARCHAR ( 30 ) NOT NULL COMMENT '平台key',
	is_disable TINYINT DEFAULT 0 COMMENT '是否禁用',
	order_status_id INT COMMENT '订单状态',
	pay_status_id INT COMMENT '支付状态',
	order_status VARCHAR(20) COMMENT '订单状态',
	pay_status VARCHAR(20) COMMENT '支付状态',	
	auth_status_id INT COMMENT '认证状态',
	auth_status VARCHAR(20) COMMENT '认证状态',
	approve_by_id int COMMENT '订单审核人id',
	approve_by VARCHAR(20) COMMENT '订单审核人',
	pay_method_id INT COMMENT '支付方式',
	pay_method VARCHAR(20) COMMENT '支付方式',
	buy_method INT COMMENT '购买方式',
	buy_period INT COMMENT '购买时长',
	final_price DECIMAL(10,2) COMMENT '成交价',
	original_price DECIMAL(10,2) COMMENT '原价',
	create_time datetime COMMENT '创建时间' ,
	update_time datetime COMMENT '修改时间' ,
	pay_time datetime COMMENT '支付时间' ,
	approve_time datetime COMMENT '审核时间' ,	
	end_time datetime COMMENT '交易完成时间' ,
	close_time datetime COMMENT '交易关闭时间' ,
	user_id bigint(30) NOT NULL COMMENT '用户id',
	user_name VARCHAR(30) NOT NULL COMMENT '用户账号', 
	user_real_name VARCHAR(30) NOT NULL COMMENT '客户真实姓名',
	remark VARCHAR(255) COMMENT '用户备注',
	PRIMARY KEY(id)
	) ENGINE = INNODB,
	charset = utf8,
	COMMENT '订单表';
	
	
DROP TABLE IF EXISTS o_ext_attr;

CREATE TABLE
IF
	NOT EXISTS o_ext_attr (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	order_id VARCHAR(30) NOT NULL COMMENT '订单id',
	skuid VARCHAR(30) COMMENT 'skuID',
	extend_attr_id VARCHAR(30) COMMENT '附加属性值id',
	extend_attr_name VARCHAR(200) COMMENT '附加属性值名称'
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '订单附加属性表';
	

DROP TABLE IF EXISTS  o_userinfo;

CREATE TABLE
IF
	NOT EXISTS o_userinfo (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	order_id VARCHAR(30) NOT NULL COMMENT '订单id',
	receive_name VARCHAR(30) NOT NULL COMMENT '收货人',
	phone CHAR(11) COMMENT '用户电话',
	email VARCHAR(100) COMMENT '用户邮箱', 
	province VARCHAR(20) COMMENT '收货省名称',
	city VARCHAR(20) COMMENT '收货市名称',
	area VARCHAR(20) COMMENT '收货区/镇名称',
	address VARCHAR(500) COMMENT '详细地址',
	zip int COMMENT '邮政编码',
	region_id INT COMMENT '区域编号'
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '订单用户表';
	
	
	
DROP TABLE IF EXISTS o_product;

CREATE TABLE
IF
	NOT EXISTS o_product (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	order_id VARCHAR(30) NOT NULL COMMENT '订单id',
	
	product_id VARCHAR(30) NOT NULL COMMENT '产品id',
	product_code VARCHAR ( 50 ) NOT NULL COMMENT '产品编码',
	product_name VARCHAR ( 100 ) NOT NULL COMMENT '产品名称',
	category VARCHAR(200) COMMENT '产品分类',
	final_price DECIMAL(10,2) COMMENT '产品成交价',
	original_price DECIMAL(10,2) COMMENT '产品原价',
	quantity INT COMMENT '产品数量',
	product_image VARCHAR ( 500 ) COMMENT '产品图片地址',
	short_desc VARCHAR ( 100 ) COMMENT '产品简述',
	description text COMMENT '产品描述',
	attr_json VARCHAR(255) COMMENT '产品属性',
	skuid VARCHAR(30) COMMENT 'skuID',
	sku_name VARCHAR(50) COMMENT '单品名称',
	sku_json VARCHAR(500) COMMENT 'sku字符串'
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '订单产品表';
	

-- ==========================================《订单表 end》===============



-- ==========================================《订单历史表 begin》===============

DROP TABLE IF EXISTS orders_history;
CREATE TABLE
IF
	NOT EXISTS orders_history (
	id VARCHAR ( 30 ) COMMENT '主键id',
	platkey VARCHAR ( 30 ) NOT NULL COMMENT '平台key',
	is_disable TINYINT DEFAULT 0 COMMENT '是否禁用',
	order_status_id INT COMMENT '订单状态',
	pay_status_id INT COMMENT '支付状态',
	order_status VARCHAR(20) COMMENT '订单状态',
	pay_status VARCHAR(20) COMMENT '支付状态',	
	auth_status_id INT COMMENT '认证状态',
	auth_status VARCHAR(20) COMMENT '认证状态',
	approve_by_id int COMMENT '订单审核人id',
	approve_by VARCHAR(20) COMMENT '订单审核人',
	pay_method_id INT COMMENT '支付方式',
	pay_method VARCHAR(20) COMMENT '支付方式',
	buy_method INT COMMENT '购买方式',
	buy_period INT COMMENT '购买时长',
	final_price DECIMAL(10,2) COMMENT '成交价',
	original_price DECIMAL(10,2) COMMENT '原价',
	create_time datetime COMMENT '创建时间' ,
	update_time datetime COMMENT '修改时间' ,
	pay_time datetime COMMENT '支付时间' ,
	approve_time datetime COMMENT '审核时间' ,	
	end_time datetime COMMENT '交易完成时间' ,
	close_time datetime COMMENT '交易关闭时间' ,
	user_id bigint(30) NOT NULL COMMENT '用户id',
	user_name VARCHAR(30) NOT NULL COMMENT '用户账号', 
	user_real_name VARCHAR(30) NOT NULL COMMENT '客户真实姓名',
	PRIMARY KEY(id)
	) ENGINE = INNODB,
	charset = utf8,
	COMMENT '订单表历史';
	
		
	
-- ==========================================《订单历史表 end》===============


	
-- ==========================================《收货地址表 begin》===============

DROP TABLE IF EXISTS delivery_address;

CREATE TABLE
IF
	NOT EXISTS delivery_address (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	user_id bigint(30) NOT NULL COMMENT '用户id',	
	user_name VARCHAR(30) NOT NULL COMMENT '用户姓名', 
	telephone CHAR(11)  COMMENT '电话',
	telephone_bak CHAR(11)  COMMENT '备用电话',
	country VARCHAR(20) COMMENT '国家',
	province VARCHAR(20) COMMENT '省份 ',
	city VARCHAR(20) COMMENT '城市',
	area VARCHAR(20) COMMENT '地区',
	street VARCHAR(200) COMMENT '街道/详细收货地址',
	zip INT COMMENT '邮政编码',
	is_default_address TINYINT COMMENT '是否默认收货地址 ',
	create_time datetime COMMENT '创建时间' 
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '收货地址表';

TRUNCATE TABLE delivery_address;
INSERT INTO `delivery_address` (
`id`,
`platkey`,
`user_id`,
`user_name`,
`telephone`,
`telephone_bak`,
`country`,
`province`,
`city`,
`area`,
`street`,
`zip`,
`is_default_address`,
`create_time` 
)
VALUES
	(
	'5c3d40f1d4fc3e17e41b0ca5',
	'AuthPlat',
	1,
	'admin',
	'18672905502',
	'18672905502',
	'Tencent',
	'广东省',
	'深圳市',
	'龙岗区',
	'大运街道',
	518000,
	1,
	'2019-01-15 10:29:16' 
	);
-- ==========================================《收货地址表 end》===============




-- ==========================================《订单退货表 begin》===============
DROP TABLE IF EXISTS order_returns;

CREATE TABLE
IF
	NOT EXISTS order_returns (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	returns_no VARCHAR(50) NOT NULL COMMENT '退货编号',	
	express_no VARCHAR(50) NOT NULL COMMENT '物流单号',
	consignee_realname VARCHAR(30) NOT NULL COMMENT '收货人姓名', 
	consignee_telphone CHAR(11)  COMMENT '联系电话',	
	consignee_telphone_bak CHAR(11)  COMMENT '备用联系电话',	
	consignee_address	VARCHAR(500) COMMENT '收货地址', 
	consignee_zip VARCHAR (10) NOT NULL COMMENT '邮政编码',
	logistics_type VARCHAR(20)  COMMENT '物流方式',
	logistics_id VARCHAR(30) COMMENT '物流商家编号',
	logistics_fee DECIMAL(10,2) COMMENT '物流发货运费',
	agency_fee DECIMAL(10,2) COMMENT '快递代收货款费率',
	delivery_amount DECIMAL(10,2) COMMENT '物流成本金额',
	orderlogistics_status VARCHAR(50) COMMENT '物流状态',
	logistics_settlement_status VARCHAR(50) COMMENT '物流结算状态',
	logistics_result_last VARCHAR(50) COMMENT '物流最后状态描述 ',
	logistics_result VARCHAR(50) COMMENT '物流描述',
	logistics_create_time datetime COMMENT '发货时间',
	logistics_update_time datetime COMMENT '物流更新时间 ',
	logistics_settlement_time datetime COMMENT '物流结算时间',
	logistics_payway VARCHAR(50) COMMENT '物流支付渠道',
	logistics_pay_no VARCHAR(50) COMMENT '物流支付单号',
	reconciliation_time datetime COMMENT '物流公司对账日期',
	reconciliation_status VARCHAR(50) COMMENT '物流公司已对账状态'
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '订单物流表';


TRUNCATE TABLE order_returns;

INSERT INTO `order_returns`(`id`, `platkey`, `returns_no`, `express_no`, `consignee_realname`, `consignee_telphone`, `consignee_telphone_bak`, `consignee_address`, `consignee_zip`, `logistics_type`, `logistics_id`, `logistics_fee`, `agency_fee`, `delivery_amount`, `orderlogistics_status`, `logistics_settlement_status`, `logistics_result_last`, `logistics_result`, `logistics_create_time`, `logistics_update_time`, `logistics_settlement_time`, `logistics_payway`, `logistics_pay_no`, `reconciliation_time`, `reconciliation_status`) VALUES ('5c3d40f1d5cv3e17ehd0ca5', 'AuthPlat',  '31dsgsd561', '165454616478', '刘伟', '18672905502', '18647154230', '福田区景田文博大厦', '518000', '快递', '164611', 51.05, 25.10, 524.56, '运输中', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

-- ==========================================《订单退货表 end》===============




-- ==========================================《状态表 begin》===============
DROP TABLE
IF
	EXISTS or_status;
CREATE TABLE
IF
	NOT EXISTS or_status (
	id INT PRIMARY KEY auto_increment COMMENT '主键',
	status_id INT COMMENT '状态id',
	status_val VARCHAR ( 20 ) COMMENT '状态值',
	status_type VARCHAR ( 20 ) COMMENT '状态类型' 
	) ENGINE = INNODB,
	charset = utf8,
	COMMENT '状态表';


TRUNCATE TABLE or_status;

INSERT INTO `or_status` ( `status_id`, `status_val`, `status_type` )
VALUES
	( -1, '已删除', 'OrderStatus' ),
	( 0, '已关闭', 'OrderStatus' ),
	( 1, '正常', 'OrderStatus' ),
	( 2, '退款中', 'OrderStatus' ),
	( 3, '退款完成', 'OrderStatus' ),
	( 4, '退款未通过', 'OrderStatus' ),
	( 5, '未支付', 'OrderStatus' ),
	( 6, '待支付尾款', 'OrderStatus' ),
	( 7, '已支付', 'OrderStatus' ),
	( 0, '支付宝', 'PayMethod' ),
	( 1, '微信', 'PayMethod' ),
	( 2, '银联', 'PayMethod' );
	
-- ==========================================《状态表 end》===============






