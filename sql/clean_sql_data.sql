USE [PostsDb];
DELETE FROM Comments;
DELETE FROM Posts;
DELETE FROM Users;

USE [LikesDb];
DELETE FROM Likes;
DELETE FROM Posts;
DELETE FROM Users;

USE [IdentityDb];
DELETE FROM AspNetUserClaims;
DELETE FROM AspNetUserLogins;
DELETE FROM AspNetUsers;
DELETE FROM AspNetUserTokens;
DELETE FROM PersistedGrants;
DELETE FROM DeviceCodes;
DELETE FROM AspNetUserRoles;