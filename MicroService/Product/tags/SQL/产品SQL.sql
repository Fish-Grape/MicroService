
-- ==========================================《购物车表 end》===============
DROP TABLE IF EXISTS shopping_cart;

CREATE TABLE
IF
	NOT EXISTS shopping_cart (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	user_id bigint(30) NOT NULL COMMENT '用户id',	
	user_name VARCHAR(30) NOT NULL COMMENT '用户姓名', 
	shop_id	VARCHAR(30) COMMENT '商店编号', 
	is_product_exists TINYINT  COMMENT '是否有效',
	number INT COMMENT '购买数量',
	productid VARCHAR (30) NOT NULL COMMENT '产品id',
	product_name VARCHAR(50) COMMENT '产品名称',
	buy_method INT COMMENT '购买方式',
	buy_period INT COMMENT '购买时长',
	skuid VARCHAR(30) COMMENT 'skuID',
	price DECIMAL(10,2) COMMENT '单品价格',
	total_price DECIMAL(10,2) COMMENT '总价',
	good_name VARCHAR(50) COMMENT '单品名称',
	create_time datetime COMMENT '创建时间' ,
	valid_type VARCHAR(30) COMMENT '审核类型'
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '购物车表';
	
	
DROP TABLE IF EXISTS shopping_cart_detail;

CREATE TABLE
IF
	NOT EXISTS shopping_cart_detail (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	shopping_cart_id VARCHAR(30) not NULL COMMENT '购物车id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	extend_attr_id VARCHAR(30) COMMENT '附加属性值id',
	extend_attr_name VARCHAR(200) COMMENT '附加属性值名称',
	create_time datetime COMMENT '创建时间' 
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '购物车详情表';

-- ==========================================《购物车表 end》===============




-- ==========================================《附加属性表 begin》===============



	
DROP TABLE IF EXISTS extend_attr;

CREATE TABLE
IF
	NOT EXISTS extend_attr (
	id VARCHAR(30) PRIMARY KEY COMMENT '主键id',
	platkey VARCHAR(30) NOT NULL COMMENT '平台key',
	ex_type INT NOT NULL COMMENT '附加类型：1:分类,2:属性,3:属性值',
	ex_id VARCHAR(30) NOT NULL COMMENT '关联id',
	ex_name VARCHAR(200) COMMENT '附加属性值',
	add_price DECIMAL(10,2) COMMENT '附加价格',
	create_time datetime COMMENT '创建时间'
) ENGINE = INNODB,
	charset = utf8,
	COMMENT '附加属性表';

-- ==========================================《附加属性表 end》===============






-- ==========================================《状态表 begin》===============
DROP TABLE
IF
	EXISTS p_status;
CREATE TABLE
IF
	NOT EXISTS p_status (
	id INT PRIMARY KEY auto_increment COMMENT '主键',
	status_id INT COMMENT '状态id',
	status_val VARCHAR ( 20 ) COMMENT '状态值',
	status_type VARCHAR ( 20 ) COMMENT '状态类型' 
	) ENGINE = INNODB,
	charset = utf8,
	COMMENT '状态表';


TRUNCATE TABLE p_status;

INSERT INTO `p_status` ( `status_id`, `status_val`, `status_type` )
VALUES
	( 1, '首付70%,领证前付30%', 'BuyMethod' ),
	( 2, '全款支付', 'BuyMethod' );
	
-- ==========================================《状态表 end》===============






