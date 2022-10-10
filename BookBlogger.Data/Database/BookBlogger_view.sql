
Create View BookView As

SELECT Books.ID, Books.ISBN, Books.BookName, Books.Price, Books.Details, Books.ImageUrl, Books.DownloadUrl, (SELECT Authors.FirstName  WHERE Books_Authors.BookID = Books.ID) as AuthorName, (SELECT Authors.LastName  WHERE Books_Authors.BookID = Books.ID) as Surname, Books.CreatedDateTime, Books.UpdatedDateTime
FROM Books
INNER JOIN 
Books_Authors ON Books.ID = Books_Authors.BookID
INNER JOIN
Authors ON Authors.ID = Books_Authors.AuthorID;
GO




		
