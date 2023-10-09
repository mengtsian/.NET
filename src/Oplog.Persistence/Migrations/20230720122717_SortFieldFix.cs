using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SortFieldFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"CREATE OR ALTER PROCEDURE GetFilteredLogs
    @FromDate       datetime2,
    @ToDate         datetime2,
    @SearchText     nvarchar(100) = NULL,
    @LogTypeIds     Ids READONLY,
    @AreaIds        Ids READONLY,
    @SubTypeIds     Ids READONLY,
    @UnitIds        Ids READONLY,
    @SortField      nvarchar(25) = NULL,
    @SortDirection  nvarchar(5) = NULL
AS

DECLARE @sql nvarchar(max) = '
SELECT *
FROM LogsView
WHERE (CreatedDate >= @FromDate AND CreatedDate <= @ToDate)
  AND (
    IsCritical = 1
    OR (1=1';

IF @SearchText IS NOT NULL
    SET @sql += '
      AND [Text] LIKE ''%'' + @SearchText + ''%''';

IF EXISTS (SELECT 1 FROM @LogTypeIds)
    SET @sql += '
      AND LogTypeId IN (SELECT Id FROM @LogTypeIds)';

IF EXISTS (SELECT 1 FROM @AreaIds)
    SET @sql += '
      AND OperationAreaId IN (SELECT Id FROM @AreaIds)';

IF EXISTS (SELECT 1 FROM @SubTypeIds)
    SET @sql += '
      AND Subtype IN (SELECT Id FROM @SubTypeIds)';

IF EXISTS (SELECT 1 FROM @UnitIds)
    SET @sql += '
      AND Unit IN (SELECT Id FROM @UnitIds)';

SET @sql += '
    )
  )
ORDER BY ' 

IF @SortField IS NOT NULL
    SET @sql += @SortField + IIF(@SortDirection = 'DESC', ' DESC', '') + ', CreatedDate' + IIF(@SortDirection = 'DESC', ' DESC', '');
ELSE
	SET @sql = 'CreatedDate' + IIF(@SortDirection = 'DESC', ' DESC', '');

EXEC sp_executesql @sql,
  N'@FromDate       datetime2,
    @ToDate         datetime2,
    @SearchText     nvarchar(100),
    @LogTypeIds     Ids READONLY,
    @AreaIds        Ids READONLY,
    @SubTypeIds     Ids READONLY,
    @UnitIds        Ids READONLY',

    @FromDate = @FromDate,
    @ToDate = @ToDate,
    @SearchText = @SearchText,
    @LogTypeIds = @LogTypeIds,
    @AreaIds = @AreaIds,
    @SubTypeIds = @SubTypeIds,
    @UnitIds = @UnitIds;
GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"CREATE OR ALTER PROCEDURE GetFilteredLogs
    @FromDate       datetime2,
    @ToDate         datetime2,
    @SearchText     nvarchar(100) = NULL,
    @LogTypeIds     Ids READONLY,
    @AreaIds        Ids READONLY,
    @SubTypeIds     Ids READONLY,
    @UnitIds        Ids READONLY,
    @SortField      nvarchar(25) = NULL,
    @SortDirection  nvarchar(5) = NULL
AS

DECLARE @sql nvarchar(max) = '
SELECT *
FROM LogsView
WHERE (CreatedDate >= @FromDate AND CreatedDate <= @ToDate)
  AND (
    IsCritical = 1
    OR (1=1';

IF @SearchText IS NOT NULL
    SET @sql += '
      AND [Text] LIKE ''%'' + @SearchText + ''%''';

IF EXISTS (SELECT 1 FROM @LogTypeIds)
    SET @sql += '
      AND LogTypeId IN (SELECT Id FROM @LogTypeIds)';

IF EXISTS (SELECT 1 FROM @AreaIds)
    SET @sql += '
      AND OperationAreaId IN (SELECT Id FROM @AreaIds)';

IF EXISTS (SELECT 1 FROM @SubTypeIds)
    SET @sql += '
      AND Subtype IN (SELECT Id FROM @SubTypeIds)';

IF EXISTS (SELECT 1 FROM @UnitIds)
    SET @sql += '
      AND Unit IN (SELECT Id FROM @UnitIds)';

SET @sql += '
    )
  )
ORDER BY
  CreatedDate' + IIF(@SortDirection = 'DESC', ' DESC', '');

IF @SortField = 'LogTypeId'
    SET @sql += ',
LogTypeId' + IIF(@SortDirection = 'DESC', ' DESC', '');

EXEC sp_executesql @sql,
  N'@FromDate       datetime2,
    @ToDate         datetime2,
    @SearchText     nvarchar(100),
    @LogTypeIds     Ids READONLY,
    @AreaIds        Ids READONLY,
    @SubTypeIds     Ids READONLY,
    @UnitIds        Ids READONLY',

    @FromDate = @FromDate,
    @ToDate = @ToDate,
    @SearchText = @SearchText,
    @LogTypeIds = @LogTypeIds,
    @AreaIds = @AreaIds,
    @SubTypeIds = @SubTypeIds,
    @UnitIds = @UnitIds;
GO");
        }
    }
}
