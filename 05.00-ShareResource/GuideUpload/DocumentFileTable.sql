create table DocumentFiles
(
    Id            int identity
        constraint PK_DocumentFile
        primary key,
    HttpLink      nvarchar(max),
    CreatedBy     nvarchar(max),
    Approved      bit,
    CreatedDate   datetime2,
--     add foreign key chỗ này
    MeetingId int
);

-- rename column
EXEC sp_rename 'dbo.DocumentFiles.MeetingId', 'GroupId', 'COLUMN';
