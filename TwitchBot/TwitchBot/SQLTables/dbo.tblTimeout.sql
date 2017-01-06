USE [twitchbotdb]
GO

/****** Object: Table [dbo].[tblTimeout] Script Date: 9/8/2016 2:10:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblTimeout] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [username]    VARCHAR (30) NOT NULL,
    [broadcaster] INT          NOT NULL,
    [timeout]     DATETIME     NOT NULL,
    [timeAdded]   DATETIME     DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblTimeout_tblBroadcaster] FOREIGN KEY ([broadcaster]) REFERENCES [dbo].[tblBroadcasters] ([Id])
);

