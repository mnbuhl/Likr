USE [PostsDb]
GO

CREATE OR ALTER TRIGGER decrease_comments_count ON Comments
AFTER DELETE
AS
IF EXISTS (SELECT * FROM DELETED)
BEGIN
	declare @post_id as varchar(max) = (SELECT PostId FROM DELETED)

	IF NOT EXISTS (SELECT * FROM Posts WHERE Id = @post_id)
		RETURN

	UPDATE Posts 
	SET CommentsCount = CommentsCount - 1
	WHERE ID = @post_id
END
GO