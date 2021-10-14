USE [PostsDb]
GO

CREATE OR ALTER TRIGGER increase_comments_count ON Comments
AFTER INSERT
AS
IF EXISTS (SELECT * FROM INSERTED)
BEGIN
	declare @post_id as varchar(max) = (SELECT PostId FROM INSERTED)

	IF NOT EXISTS (SELECT * FROM Posts WHERE Id = @post_id)
		RETURN

	UPDATE Posts 
	SET CommentsCount = CommentsCount + 1
	WHERE ID = @post_id
END
GO