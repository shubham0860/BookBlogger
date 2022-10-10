--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@STORED PROCEDURE FOR ADDING USER@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
--Drop Procedure dbo.AddUser
CREATE PROCEDURE dbo.AddUser
    @Username VARCHAR(40),
    @Password VARCHAR(60), 
    @FirstName VARCHAR(40) = NULL, 
    @LastName VARCHAR(40) = NULL,
	@IsAdmin bit,
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
	    DECLARE @CreatedDateTime DateTime = GETDATE()
		DECLARE @UpdatedDateTime DateTime = GETDATE()
        INSERT INTO dbo.[Users] (Username, PasswordHash, FirstName, LastName, IsAdmin, CreatedDateTime)
        VALUES(@Username, HASHBYTES('SHA2_512', @Password), @FirstName, @LastName, @IsAdmin, @CreatedDateTime)

        SET @responseMessage='Success'
    END TRY

	BEGIN CATCH 
	   SET @responseMessage = 'Error During Data Insertion'
	END CATCH
END
GO
--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@STORED PROCEDURE FOR ADDING BOOKS@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
--DROP PROCEDURE dbo.AddAuthor
CREATE PROCEDURE dbo.AddAuthor
    @BookID INT,
    @FirstName VARCHAR(40) = NULL, 
    @LastName VARCHAR(40) = NULL,
	@CreatedDateTime DateTime = NUll,
	@UpdatedDateTime DateTime = NULL

AS
BEGIN
    SET NOCOUNT ON

	BEGIN TRANSACTION

    BEGIN TRY
	    	INSERT INTO dbo.[Authors] (FirstName, LastName,CreatedDateTime,UpdatedDateTime)
            VALUES(@FirstName, 
			       @LastName,
				   @CreatedDateTime,
				   @UpdatedDateTime)
			
            DECLARE @AuthorID INT
			SET @AuthorID = @@IDENTITY

		    INSERT INTO dbo.[Books_Authors] (BookID,AuthorID ,CreatedDateTime)
                    VALUES(@BookID, 
					       @AuthorID,
	                       @CreatedDateTime)

          COMMIT TRANSACTION
    END TRY
	BEGIN CATCH
			ROLLBACK
	END CATCH
END
GO

--Drop Procedure dbo.AddBook
CREATE PROCEDURE dbo.AddBook
    @UserID INT,
    @ISBN VARCHAR(60), 
    @BookName VARCHAR(40),
	@Price DECIMAL(7,2),
	@Details VARCHAR(255),
    @ImageUrl VARCHAR(150) = NULL,
	@DownloadUrl VARCHAR(150) NULL,
	@FirstName VARCHAR(40),
	@LastName VARCHAR(40),
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	BEGIN TRANSACTION

    BEGIN TRY
	    
		DECLARE @CreatedDateTime DateTime = GETDATE();
		DECLARE @UpdatedDateTime DateTime = GETDATE(); 
        INSERT INTO dbo.[Books] (ISBN, BookName, Price, Details, ImageUrl, DownloadUrl, CreatedDateTime, UpdatedDateTime)
        VALUES(
    --@UserID, 
    @ISBN, 
    @BookName,
	@Price,
	@Details,
    @ImageUrl,
	@DownloadUrl,
	@CreatedDateTime,
	@UpdatedDateTime)
	    
		DECLARE @BookID INT
		SET @BookID = @@IDENTITY


		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		VALUES (@UserID,@BookID,'Insert',@BookName,'Not Applicable',@CreatedDateTime)

		INSERT INTO dbo.[Users_Books] (UserID,BookID,CreatedDateTime) 
		VALUES (@UserID,@BookID,@CreatedDateTime)



		
		   EXEC dbo.AddAuthor
               @BookID = @BookID,
               @FirstName = @FirstName,
	           @LastName = @LastName,
	           @CreatedDateTime = @CreatedDateTime,
			   @UpdatedDateTime = @UpdatedDateTime


        SET @responseMessage='Success'
		COMMIT TRANSACTION
    END TRY
	BEGIN CATCH
        
	    ROLLBACK
	END CATCH
END
GO
--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

--DROP PROCEDURE dbo.EditBook
CREATE PROCEDURE dbo.EditBook
    @UserID INT,
    @BookID INT,
    @ISBN VARCHAR(60), 
    @BookName VARCHAR(40),
	@Price DECIMAL(7,2),
	@Details VARCHAR(255),
    @ImageUrl VARCHAR(150) = NULL,
	@DownloadUrl VARCHAR(150) NULL,
	@AuthorName VARCHAR(40),
	@Surname VARCHAR(40),
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	BEGIN TRANSACTION
    BEGIN TRY
	    DECLARE @UpdatedDateTime DateTime = GETDATE();
		DECLARE @CreatedDateTime DateTime = GETDATE();

		DECLARE @OldBook VARCHAR(50)
		SET @OldBook = (SELECT BookName FROM Books WHERE ID=@BookID)

		DECLARE @OldPrice Decimal(7,2)
		SET @OldPrice = (SELECT Price FROM Books WHERE ID=@BookID)

	    DECLARE @OldDetails VARCHAR(255)
		SET @OldDetails = (SELECT Details FROM Books WHERE ID=@BookID)

	    DECLARE @OldImageUrl VARCHAR(150)
		SET @OldImageUrl = (SELECT ImageUrl FROM Books WHERE ID=@BookID)

	    DECLARE @OldDownloadUrl VARCHAR(150)
		SET @OldDownloadUrl = (SELECT DownloadUrl FROM Books WHERE ID=@BookID)

		DECLARE @AuthorID INT;
		SET @AuthorID = (SELECT Authors.ID
        FROM Books
        INNER JOIN 
         Books_Authors ON Books.ID = Books_Authors.BookID
          INNER JOIN
         Authors ON Authors.ID = Books_Authors.AuthorID WHERE Books.ID = @BookID);

		DECLARE @OldAuthor VARCHAR(40)
		SET @OldAuthor = (SELECT FirstName FROM Authors WHERE ID=@AuthorID)

	    DECLARE @OldSurname VARCHAR(40)
		SET @OldSurname = (SELECT LastName FROM Authors WHERE ID=@AuthorID)

		DECLARE @OldISBN VARCHAR(60)
		SET @OldISBN = (SELECT ISBN FROM Books WHERE Books.ID = @BookID)

        UPDATE dbo.[Books] SET ISBN = @ISBN, 
		BookName=@BookName, 
		Price = @Price, 
		Details = @Details, 
		ImageUrl = @ImageUrl, 
		DownloadUrl = @DownloadUrl, 
		UpdatedDateTime = @UpdatedDateTime
		WHERE ID = @BookID;

		Update dbo.[Authors] SET 
		FirstName = @AuthorName,
		LastName = @Surname
		WHERE ID = @AuthorID;
		
		IF (@OldISBN != @ISBN)
		BEGIN
		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		VALUES (@UserID,@BookID,'Updating ISBN',@ISBN,@OldISBN,@CreatedDateTime)
		END

        IF (@OldBook != @BookName)
		BEGIN
		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		VALUES (@UserID,@BookID,'Updating Book',@BookName,@OldBook,@CreatedDateTime)
		END

		IF (@OldPrice != @Price)
		 BEGIN;
		    		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		            VALUES (@UserID,@BookID,'Updating Price',@Price,@OldPrice,@CreatedDateTime)
		 END;

		 IF (@OldDetails != @Details)
		 BEGIN;
		    		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		            VALUES (@UserID,@BookID,'Updating Details',@Details,@OldDetails,@CreatedDateTime)
		 END;

		 IF (@OldImageUrl != @ImageUrl)
		 BEGIN;
		    		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		            VALUES (@UserID,@BookID,'Updating URL',@ImageUrl,@OldImageUrl,@CreatedDateTime)
		 END;

		 IF (@OldDownloadUrl != @DownloadUrl)
		 BEGIN;
		    		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		            VALUES (@UserID,@BookID,'Updating URL',@DownloadUrl,@OldDownloadUrl,@CreatedDateTime)
		 END;

		IF (@OldAuthor != @AuthorName)
		BEGIN
		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		VALUES (@UserID,@BookID,'Updating Author',@AuthorName,@OldAuthor,@CreatedDateTime)
		END

		IF (@OldSurname != @Surname)
		BEGIN
		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		VALUES (@UserID,@BookID,'Updating Book',@Surname,@OldSurname,@CreatedDateTime)
		END
        
		SET @responseMessage='Success'
		COMMIT TRANSACTION
    END TRY
	BEGIN CATCH
        SET @responseMessage='Updating Book Failed'
		ROLLBACK
	END CATCH
END
GO
--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

--DROP PRoCEDURE dbo.DeleteBook
CREATE PROCEDURE dbo.DeleteBook
    @UserID INT,
	@BookID INT,
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	BEGIN TRANSACTION
    BEGIN TRY

		DELETE FROM dbo.[Users_Books]
		WHERE BookID = @BookID

	    DECLARE @Author INT;

		SET @Author = (SELECT AuthorID FROM dbo.[Books_Authors] 
		WHERE BookID = @BookID)

		DELETE FROM dbo.[Books_Authors] 
		WHERE BookID = @BookID

	    DECLARE @BookName VARCHAR(50)
		SET @BookName = (SELECT BookName FROM Books WHERE ID = @BookID)

		DELETE FROM dbo.[Books] 
		WHERE ID = @BookID;

		DECLARE @Username VARCHAR(50)
		SET @Username = (SELECT Username FROM Users WHERE ID = @UserID)

		--DECLARE @BookName VARCHAR(50)
		--SET @BookName = (SELECT BookName FROM Books WHERE ID = @BookID)
		DECLARE @CreatedDateTime DateTime = GETDATE();

		INSERT INTO dbo.[Audit](UserID, BookID, Operation, NewValue, OldValue, CreatedDateTime)
		VALUES (@UserID,@BookID,'Deleting Book','Not Applicable',@BookName,@CreatedDateTime)

	    DELETE FROM dbo.[Authors]
		WHERE ID = @Author

        SET @responseMessage='Success'
		COMMIT TRANSACTION
    END TRY
	BEGIN CATCH
        SET @responseMessage='Deleting Book Failed'
		ROLLBACK
	END CATCH
END
GO
--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

--Drop Procedure dbo.ReadBooks
CREATE PROCEDURE dbo.ReadBooks
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY

	SELECT * FROM dbo.BookView;
	   

        SET @responseMessage='Success'

    END TRY
	BEGIN CATCH
        SET @responseMessage='Reading Book Failed'

	END CATCH
END
GO

CREATE PROCEDURE dbo.ReadBook
    @UserID INT,
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY

	SELECT Books.ID, Books.ISBN, Books.BookName, Books.Price, Books.Details, Books.ImageUrl, Books.DownloadUrl, (SELECT Authors.FirstName  WHERE Books_Authors.BookID = Books.ID) as AuthorName, (SELECT Authors.LastName  WHERE Books_Authors.BookID = Books.ID) as Surname, Books.CreatedDateTime, Books.UpdatedDateTime
   FROM Users
   INNER JOIN
   Users_Books ON Users.ID = Users_Books.UserID
   INNER JOIN
   Books ON Books.ID = Users_Books.BookID
   INNER JOIN 
   Books_Authors ON Books.ID = Books_Authors.BookID
   INNER JOIN
   Authors ON Authors.ID = Books_Authors.AuthorID
   WHERE Users.ID = @UserID;
	   

        SET @responseMessage='Success'

    END TRY
	BEGIN CATCH
        SET @responseMessage='Reading Book Failed'

	END CATCH
END
GO
--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


--Drop Procedure ReadAudit
CREATE PROCEDURE dbo.ReadAudit
@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRY
	SELECT Username, BookName, Operation, NewValue, OldValue, Audit.CreatedDateTime FROM Users Inner JOIN Audit ON Users.ID = Audit.UserID INNER JOIN Books ON Audit.BookID = Books.ID

    END TRY
	BEGIN CATCH
	   SET @responseMessage='Reading Audit Failed'    
	END CATCH
END
GO
