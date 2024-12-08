USE [TUMITICDB]
GO
BEGIN TRANSACTION
	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICMPersonal')
	BEGIN 
		DROP TABLE [dbo].[TICMPersonal]
	END
	
	CREATE TABLE [dbo].[TICMPersonal](
		cCodPerson		CHAR(6) 			NOT NULL,
		cNumDocIde		VARCHAR(8)			NOT NULL,
		cNomPerson		VARCHAR(200)		NOT NULL,
		dFecNacPer		DATETIME			NOT NULL,
		cCorEleIns		VARCHAR(150)		NOT NULL,
		cCodOficin		CHAR(3)				NOT NULL,
		cCodUniOrg		CHAR(3)				NOT NULL,
		cCodGruPer		CHAR(3)				NOT NULL,
		cClaEncPer		VARCHAR(150)		NOT NULL,
		cCodEstPer		CHAR(1)				NOT NULL,
		lIdeColAut		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
		cCodUsuMod		CHAR(6)				NULL,
		dFecUsuMod		DATETIME			NULL,
		cAuditUser		VARCHAR(30)			DEFAULT LEFT(SUSER_SNAME(),30),
		cAuditStation	VARCHAR(30)			DEFAULT LEFT(HOST_NAME(),30),
		cAuditClient	VARCHAR(50)			DEFAULT LEFT(APP_NAME(),50),
		dAuditDate		DATETIME			DEFAULT (GETDATE()),
	CONSTRAINT [TICMPersonal_cCodPerson_CPK] PRIMARY KEY CLUSTERED 
	(
		[cCodPerson] ASC
	)
	) ON [PRIMARY]
	

	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTCargo')
	BEGIN 
		DROP TABLE [dbo].[TICTCargo]
	END
	
	CREATE TABLE [dbo].[TICTCargo](
		cCodGruPer		CHAR(3) 			NOT NULL,
		cDesGruPer		VARCHAR(200)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
		cCodUsuMod		CHAR(6)				NULL,
		dFecUsuMod		DATETIME			NULL,
		cAuditUser		VARCHAR(30)			DEFAULT LEFT(SUSER_SNAME(),30),
		cAuditStation	VARCHAR(30)			DEFAULT LEFT(HOST_NAME(),30),
		cAuditClient	VARCHAR(50)			DEFAULT LEFT(APP_NAME(),50),
		dAuditDate		DATETIME			DEFAULT (GETDATE()),
	CONSTRAINT [TICTCargo_cCodGruPer_CPK] PRIMARY KEY CLUSTERED 
	(
		[cCodGruPer] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTUnidadOrganica')
	BEGIN 
		DROP TABLE [dbo].[TICTUnidadOrganica]
	END
	
	CREATE TABLE [dbo].[TICTUnidadOrganica](
		cCodUniOrg		CHAR(3) 			NOT NULL,
		cDesUniOrg		VARCHAR(200)		NOT NULL,
		lEstIdeInc		BIT					NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
		cCodUsuMod		CHAR(6)				NULL,
		dFecUsuMod		DATETIME			NULL,
		cAuditUser		VARCHAR(30)			DEFAULT LEFT(SUSER_SNAME(),30),
		cAuditStation	VARCHAR(30)			DEFAULT LEFT(HOST_NAME(),30),
		cAuditClient	VARCHAR(50)			DEFAULT LEFT(APP_NAME(),50),
		dAuditDate		DATETIME			DEFAULT (GETDATE()),
	CONSTRAINT [TICTUnidadOrganica_cCodUniOrg_CPK] PRIMARY KEY CLUSTERED 
	(
		[cCodUniOrg] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTOficina')
	BEGIN 
		DROP TABLE [dbo].[TICTOficina]
	END
	
	CREATE TABLE [dbo].[TICTOficina](
		cCodOficin		CHAR(3) 			NOT NULL,
		cDesOficin		VARCHAR(200)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
		cCodUsuMod		CHAR(6)				NULL,
		dFecUsuMod		DATETIME			NULL,
		cAuditUser		VARCHAR(30)			DEFAULT LEFT(SUSER_SNAME(),30),
		cAuditStation	VARCHAR(30)			DEFAULT LEFT(HOST_NAME(),30),
		cAuditClient	VARCHAR(50)			DEFAULT LEFT(APP_NAME(),50),
		dAuditDate		DATETIME			DEFAULT (GETDATE()),
	CONSTRAINT [TICTOficina_cCodOficin_CPK] PRIMARY KEY CLUSTERED 
	(
		[cCodOficin] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTSubUnidadOrganica')
	BEGIN 
		DROP TABLE [dbo].[TICTSubUnidadOrganica]
	END
	
	CREATE TABLE [dbo].[TICTSubUnidadOrganica](
		cCodSubUni		CHAR(3) 			NOT NULL,
		cCodUniOrg		CHAR(3)				NOT NULL,
		cDesSubUni		VARCHAR(200)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
	CONSTRAINT [TICTSubUnidadOrganica_cCodSubUni_CPK] PRIMARY KEY CLUSTERED 
	(
		[cCodSubUni] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTCategoriaSubUnidad')
	BEGIN 
		DROP TABLE [dbo].[TICTCategoriaSubUnidad]
	END
	
	CREATE TABLE [dbo].[TICTCategoriaSubUnidad](
		cCodCatAre		CHAR(3) 			NOT NULL,
		cCodSubUni		CHAR(3)				NOT NULL,
		cDesCatAre		VARCHAR(200)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
	CONSTRAINT [TICTCategoriaSubUnidad_cCodCatAre_CPK] PRIMARY KEY CLUSTERED 
	(
		[cCodCatAre] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTSubCategoria')
	BEGIN 
		DROP TABLE [dbo].[TICTSubCategoria]
	END
	
	CREATE TABLE [dbo].[TICTSubCategoria](
		cCodSubCat		CHAR(3) 			NOT NULL,
		cCodCatAre		CHAR(3)				NOT NULL,
		cDesSubCat		VARCHAR(200)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
	CONSTRAINT [TICTSubCategoria_cCodSubCat_CPK] PRIMARY KEY CLUSTERED 
	(
		[cCodSubCat] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTLugarIncidente')
	BEGIN 
		DROP TABLE [dbo].[TICTLugarIncidente]
	END
	
	CREATE TABLE [dbo].[TICTLugarIncidente](
		nCodLugInc		INT IDENTITY		NOT NULL,
		cDesLugInc		VARCHAR(200)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
	CONSTRAINT [TICTLugarIncidente_nCodLugInc_CPK] PRIMARY KEY CLUSTERED 
	(
		[nCodLugInc] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICMTicket')
	BEGIN 
		DROP TABLE [dbo].[TICMTicket]
	END
	
	CREATE TABLE [dbo].[TICMTicket](
		nCodTicket		INT IDENTITY		NOT NULL,
		cCorPerReg		VARCHAR(200)		NOT NULL,
		cCodUniOrg		CHAR(3)				NOT NULL,
		cCodSubUni		CHAR(3)				NULL,
		cCodCatAre		CHAR(3)				NULL,
		cCodSubCat		CHAR(3)				NULL,
		cDesTicket		VARCHAR(500)		NOT NULL,
		cNumCelReg		VARCHAR(15)			NOT NULL,
		nCodLugInc		INT					NOT NULL,
		cCodResAte		CHAR(6)				NOT NULL,
		lIndSegInf		BIT					NULL,
		lIndConNeg		BIT					NULL,
		cNomArcAdj		VARCHAR(70)			NULL,
		cCodDerTic		CHAR(6)				NULL,
		dFecFinAte		DATETIME			NULL,
		lEstAteTic		BIT					NULL,
		cAccCieTic		VARCHAR(MAX)		NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
		cCodUsuMod		CHAR(6)				NULL,
		dFecUsuMod		DATETIME			NULL,
	CONSTRAINT [TICMTicket_nCodTicket_CPK] PRIMARY KEY CLUSTERED 
	(
		[nCodTicket] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICTTipoEvidencia')
	BEGIN 
		DROP TABLE [dbo].[TICTTipoEvidencia]
	END

	CREATE TABLE [dbo].[TICTTipoEvidencia](
		nCodTipEvi		INT IDENTITY		NOT NULL,
		cDesTipEvi		VARCHAR(200)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
	CONSTRAINT [TICTTipoEvidencia_nCodTipEvi_CPK] PRIMARY KEY CLUSTERED 
	(
		[nCodTipEvi] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	IF EXISTS(SELECT * FROM SYS.TABLES WHERE name = 'TICDTicketDocumento')
	BEGIN 
		DROP TABLE [dbo].[TICDTicketDocumento]
	END

	CREATE TABLE [dbo].[TICDTicketDocumento](
		nCodDocTic		INT IDENTITY		NOT NULL,
		nCodTipEvi		INT					NOT NULL,
		nCodTicket		INT					NOT NULL,
		cNomArcTic		VARCHAR(250)		NOT NULL,
		lEstRegist		BIT					NOT NULL,
		cCodUsuReg		CHAR(6)				NOT NULL,
		dFecUsuReg		DATETIME			NOT NULL,
		cCodUsuMod		CHAR(6)				NULL,
		dFecUsuMod		DATETIME			NULL,
	CONSTRAINT [TICDTicketDocumento_nCodDocTic_CPK] PRIMARY KEY CLUSTERED 
	(
		[nCodDocTic] ASC
	)
	) ON [PRIMARY]
	
	IF @@ERROR > 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
COMMIT TRANSACTION