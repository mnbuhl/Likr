USE [PostsDb]
GO

CREATE OR ALTER TRIGGER increase_comments_count ON Comments
AFTER INSERT
AS
IF EXISTS (SELECT * FROM INSERTED)
BEGIN
	DECLARE @post_id AS VARCHAR(MAX)

	DECLARE c_cursor CURSOR FOR 
	SELECT PostId FROM INSERTED

	OPEN c_cursor

	FETCH NEXT FROM c_cursor INTO @post_id

	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF NOT EXISTS (SELECT * FROM Posts WHERE Id = @post_id)
			FETCH NEXT FROM c_cursor INTO @post_id
		ELSE
			UPDATE Posts 
			SET CommentsCount = CommentsCount + 1
			WHERE ID = @post_id

			FETCH NEXT FROM c_cursor INTO @post_id
	END
	CLOSE c_cursor
	DEALLOCATE c_cursor
END
GO