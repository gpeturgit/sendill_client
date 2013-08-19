
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/12/2013 14:55:30
-- Generated from EDMX file: C:\Users\EinarGunnar\Documents\workspace\sendillonice\sendill_client\sendill_client\SendillContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbsendill];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tbl_cars_tbl_driver_ledgers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_driver_ledgers] DROP CONSTRAINT [FK_tbl_cars_tbl_driver_ledgers];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_carstbl_driver_customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_driver_customers] DROP CONSTRAINT [FK_tbl_carstbl_driver_customers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbsendillModelStoreContainer].[BÍLAR]', 'U') IS NOT NULL
    DROP TABLE [dbsendillModelStoreContainer].[BÍLAR];
GO
IF OBJECT_ID(N'[dbo].[tbl_cars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_cars];
GO
IF OBJECT_ID(N'[dbo].[tbl_customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_customers];
GO
IF OBJECT_ID(N'[dbo].[tbl_driver_customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_driver_customers];
GO
IF OBJECT_ID(N'[dbo].[tbl_driver_ledger_rec]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_driver_ledger_rec];
GO
IF OBJECT_ID(N'[dbo].[tbl_driver_ledger_record_type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_driver_ledger_record_type];
GO
IF OBJECT_ID(N'[dbo].[tbl_driver_ledgers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_driver_ledgers];
GO
IF OBJECT_ID(N'[dbo].[tbl_driver_users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_driver_users];
GO
IF OBJECT_ID(N'[dbo].[tbl_ledger_entry_types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ledger_entry_types];
GO
IF OBJECT_ID(N'[dbo].[tbl_pinnames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_pinnames];
GO
IF OBJECT_ID(N'[dbo].[tbl_pins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_pins];
GO
IF OBJECT_ID(N'[dbo].[tbl_tours]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_tours];
GO
IF OBJECT_ID(N'[dbo].[tbl_vsk_periods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_vsk_periods];
GO
IF OBJECT_ID(N'[dbo].[tbl_vsk_types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_vsk_types];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbl_cars'
CREATE TABLE [dbo].[tbl_cars] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [stationid] smallint  NULL,
    [carnumber] nvarchar(255)  NULL,
    [code] nvarchar(255)  NULL,
    [listed] bit  NULL,
    [carname] nvarchar(255)  NULL,
    [car1] bit  NULL,
    [car2] bit  NULL,
    [car3] bit  NULL,
    [car4] bit  NULL,
    [car5] bit  NULL,
    [car6] bit  NULL,
    [car7] bit  NULL,
    [car8] bit  NULL,
    [car9] bit  NULL,
    [car10] bit  NULL,
    [length] float  NULL,
    [back_door_width] float  NULL,
    [back_door_height] float  NULL,
    [side_door_width] float  NULL,
    [side_door_height] float  NULL,
    [weight_limit] float  NULL,
    [lift_size] smallint  NULL,
    [Volume] float  NULL,
    [width] float  NULL,
    [model] nvarchar(255)  NULL,
    [max_carry] float  NULL,
    [owner] nvarchar(255)  NULL,
    [kt] nvarchar(255)  NULL,
    [address] nvarchar(255)  NULL,
    [town] nvarchar(255)  NULL,
    [postcode] nvarchar(255)  NULL,
    [phone] nvarchar(255)  NULL,
    [mobile] nvarchar(255)  NULL,
    [driver] nvarchar(255)  NULL,
    [dkt] nvarchar(255)  NULL,
    [daddress] nvarchar(255)  NULL,
    [dtown] nvarchar(255)  NULL,
    [dpostcode] nvarchar(255)  NULL,
    [dphone] nvarchar(255)  NULL,
    [dmobile] nvarchar(255)  NULL,
    [heightofbox] float  NULL
);
GO

-- Creating table 'BÍLAR'
CREATE TABLE [dbo].[BÍLAR] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [A_Hurda_breidd] float  NULL,
    [A_Hurda_haed] float  NULL,
    [Bilnumer] nvarchar(255)  NULL,
    [Breidd] float  NULL,
    [Burdargeta] float  NULL,
    [Dregur_startar] nvarchar(255)  NULL,
    [Fjoldi] float  NULL,
    [Flytur_lengst] nvarchar(255)  NULL,
    [Heimili_bilstjora] nvarchar(255)  NULL,
    [Heimili_eiganda] nvarchar(255)  NULL,
    [H_simi_bilstjora] nvarchar(255)  NULL,
    [Hurda_breidd] float  NULL,
    [Hurda_haed] float  NULL,
    [H_simi_bilstjora2] nvarchar(255)  NULL,
    [H_simi_eiganda] nvarchar(255)  NULL,
    [H_simi_eiganda2] nvarchar(255)  NULL,
    [Kassahæð] float  NULL,
    [Koda_flokkar] nvarchar(255)  NULL,
    [Kt_Bilstjora] nvarchar(255)  NULL,
    [Kt_eiganda] nvarchar(255)  NULL,
    [Lengd] float  NULL,
    [Lyfta] nvarchar(255)  NULL,
    [Lyfta_staerd] float  NULL,
    [Nafn_bilstjora] nvarchar(255)  NULL,
    [Nafn_eiganda] nvarchar(255)  NULL,
    [Nr] float  NULL,
    [Postnr_bilstjora] nvarchar(255)  NULL,
    [Postnr_eiganda] float  NULL,
    [Rummal] float  NULL,
    [Simi_i_bil] nvarchar(255)  NULL,
    [Skilaboð] nvarchar(255)  NULL,
    [Skilaboð_dags] datetime  NULL,
    [Skilaboð_timi] datetime  NULL,
    [Skuldar] nvarchar(255)  NULL,
    [Stadur_bilstjora] nvarchar(255)  NULL,
    [Stadur_eiganda] nvarchar(255)  NULL,
    [Stardar_Flokkur] float  NULL,
    [Óskoðaður] nvarchar(255)  NULL,
    [Tegurnd] nvarchar(255)  NULL,
    [Vinnu_field] float  NULL
);
GO

-- Creating table 'tbl_customers'
CREATE TABLE [dbo].[tbl_customers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [custname] nvarchar(100)  NULL,
    [kt] nvarchar(255)  NULL,
    [address] nvarchar(100)  NULL,
    [town] nvarchar(100)  NULL,
    [postcode] nvarchar(20)  NULL,
    [phone] nvarchar(20)  NULL,
    [fax] nvarchar(255)  NULL,
    [email] nvarchar(255)  NULL,
    [vefur] nvarchar(255)  NULL,
    [contact1] nvarchar(100)  NULL,
    [phone1] nvarchar(20)  NULL,
    [mobile1] nvarchar(20)  NULL,
    [contact2] nvarchar(100)  NULL,
    [phone2] nvarchar(20)  NULL,
    [mobile2] nvarchar(20)  NULL
);
GO

-- Creating table 'tbl_driver_customers'
CREATE TABLE [dbo].[tbl_driver_customers] (
    [Id] int  NOT NULL,
    [name] nvarchar(50)  NULL,
    [address] nvarchar(50)  NULL,
    [postgode] nvarchar(5)  NULL,
    [address1] nvarchar(50)  NULL,
    [phone] nvarchar(11)  NULL,
    [mobile] nvarchar(11)  NULL,
    [email] nvarchar(50)  NULL,
    [login] nvarchar(15)  NULL,
    [password] nvarchar(15)  NULL,
    [tbl_carsID] int  NOT NULL
);
GO

-- Creating table 'tbl_driver_ledger_rec'
CREATE TABLE [dbo].[tbl_driver_ledger_rec] (
    [Id] int  NOT NULL,
    [ldate] datetime  NULL,
    [id_customer] int  NULL,
    [id_ledger] int  NULL,
    [id_period] int  NULL,
    [id_record_type] int  NULL
);
GO

-- Creating table 'tbl_driver_ledger_record_type'
CREATE TABLE [dbo].[tbl_driver_ledger_record_type] (
    [Id] int  NOT NULL,
    [code] nvarchar(15)  NULL,
    [debit] bit  NULL,
    [description] varchar(100)  NULL
);
GO

-- Creating table 'tbl_driver_ledgers'
CREATE TABLE [dbo].[tbl_driver_ledgers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [datefrom] datetime  NOT NULL,
    [dateto] datetime  NOT NULL,
    [name] nvarchar(15)  NOT NULL,
    [income] real  NULL,
    [cost] real  NULL,
    [vsk] real  NULL,
    [note] nvarchar(50)  NULL,
    [owner_id] int  NOT NULL,
    [period_id] int  NOT NULL,
    [is_open] bit  NOT NULL
);
GO

-- Creating table 'tbl_driver_users'
CREATE TABLE [dbo].[tbl_driver_users] (
    [Id] int  NOT NULL,
    [name] nvarchar(50)  NULL,
    [login] nvarchar(10)  NULL,
    [password] nvarchar(10)  NULL,
    [code] nvarchar(15)  NULL,
    [isdelete] bit  NULL,
    [contact_id] int  NULL
);
GO

-- Creating table 'tbl_ledger_entry_types'
CREATE TABLE [dbo].[tbl_ledger_entry_types] (
    [Id] int  NOT NULL,
    [code] nvarchar(10)  NULL,
    [description] nvarchar(50)  NULL
);
GO

-- Creating table 'tbl_pinnames'
CREATE TABLE [dbo].[tbl_pinnames] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [names] nvarchar(100)  NULL,
    [rows] smallint  NULL
);
GO

-- Creating table 'tbl_pins'
CREATE TABLE [dbo].[tbl_pins] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [idpin] smallint  NULL,
    [idcar] int  NULL,
    [foodbreak] bit  NULL,
    [carcode] nvarchar(10)  NULL,
    [line] smallint  NULL
);
GO

-- Creating table 'tbl_tours'
CREATE TABLE [dbo].[tbl_tours] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [id_customers] smallint  NULL,
    [id_cars] smallint  NULL,
    [id_owner] int  NULL,
    [timedate] datetime  NULL,
    [customer] nvarchar(255)  NULL,
    [address] nvarchar(255)  NULL,
    [contact] nvarchar(255)  NULL,
    [phione] nvarchar(255)  NULL,
    [note] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_vsk_periods'
CREATE TABLE [dbo].[tbl_vsk_periods] (
    [Id] int  NOT NULL,
    [code] nvarchar(10)  NULL,
    [date_from] nchar(10)  NULL,
    [date_to] nchar(10)  NULL,
    [description] nchar(10)  NULL
);
GO

-- Creating table 'tbl_vsk_types'
CREATE TABLE [dbo].[tbl_vsk_types] (
    [Id] int  NOT NULL,
    [code] nvarchar(10)  NULL,
    [calcnum] real  NULL,
    [description] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'tbl_cars'
ALTER TABLE [dbo].[tbl_cars]
ADD CONSTRAINT [PK_tbl_cars]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BÍLAR'
ALTER TABLE [dbo].[BÍLAR]
ADD CONSTRAINT [PK_BÍLAR]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_customers'
ALTER TABLE [dbo].[tbl_customers]
ADD CONSTRAINT [PK_tbl_customers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_driver_customers'
ALTER TABLE [dbo].[tbl_driver_customers]
ADD CONSTRAINT [PK_tbl_driver_customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_driver_ledger_rec'
ALTER TABLE [dbo].[tbl_driver_ledger_rec]
ADD CONSTRAINT [PK_tbl_driver_ledger_rec]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_driver_ledger_record_type'
ALTER TABLE [dbo].[tbl_driver_ledger_record_type]
ADD CONSTRAINT [PK_tbl_driver_ledger_record_type]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'tbl_driver_ledgers'
ALTER TABLE [dbo].[tbl_driver_ledgers]
ADD CONSTRAINT [PK_tbl_driver_ledgers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_driver_users'
ALTER TABLE [dbo].[tbl_driver_users]
ADD CONSTRAINT [PK_tbl_driver_users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_ledger_entry_types'
ALTER TABLE [dbo].[tbl_ledger_entry_types]
ADD CONSTRAINT [PK_tbl_ledger_entry_types]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_pinnames'
ALTER TABLE [dbo].[tbl_pinnames]
ADD CONSTRAINT [PK_tbl_pinnames]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_pins'
ALTER TABLE [dbo].[tbl_pins]
ADD CONSTRAINT [PK_tbl_pins]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_tours'
ALTER TABLE [dbo].[tbl_tours]
ADD CONSTRAINT [PK_tbl_tours]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_vsk_periods'
ALTER TABLE [dbo].[tbl_vsk_periods]
ADD CONSTRAINT [PK_tbl_vsk_periods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_vsk_types'
ALTER TABLE [dbo].[tbl_vsk_types]
ADD CONSTRAINT [PK_tbl_vsk_types]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [owner_id] in table 'tbl_driver_ledgers'
ALTER TABLE [dbo].[tbl_driver_ledgers]
ADD CONSTRAINT [FK_tbl_cars_tbl_driver_ledgers]
    FOREIGN KEY ([owner_id])
    REFERENCES [dbo].[tbl_cars]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_cars_tbl_driver_ledgers'
CREATE INDEX [IX_FK_tbl_cars_tbl_driver_ledgers]
ON [dbo].[tbl_driver_ledgers]
    ([owner_id]);
GO

-- Creating foreign key on [tbl_carsID] in table 'tbl_driver_customers'
ALTER TABLE [dbo].[tbl_driver_customers]
ADD CONSTRAINT [FK_tbl_carstbl_driver_customers]
    FOREIGN KEY ([tbl_carsID])
    REFERENCES [dbo].[tbl_cars]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_carstbl_driver_customers'
CREATE INDEX [IX_FK_tbl_carstbl_driver_customers]
ON [dbo].[tbl_driver_customers]
    ([tbl_carsID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------