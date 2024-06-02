using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTHADotNetCore.WinFormsApp.Query
{
    public class BlogQuery
    {
        public static string BlogCreate {get;} = @"INSERT INTO [dbo].[Tbl_Blog]
                                           ([BlogTitle]
                                           ,[BlogAuthor]
                                           ,[BlogContent])
                                            VALUES
                                           (
                                           @BlogTitle,
		                                   @BlogAuthor,
		                                   @BlogContent
                                           )";

        public static string BlogList { get; } = @"SELECT [BlogId]
                                                  ,[BlogTitle]
                                                  ,[BlogAuthor]
                                                  ,[BlogContent]
                                                  FROM [DotNetBatch4].[dbo].[Tbl_Blog]";
    }
}
