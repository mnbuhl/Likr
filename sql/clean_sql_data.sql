USE [PostsDb];
DELETE FROM Users;
DELETE FROM Comments;
DELETE FROM Posts;

USE [LikesDb];
DELETE FROM Users;
DELETE FROM Posts;
DELETE FROM Likes;

USE [IdentityDb];
DELETE FROM AspNetUserClaims;
DELETE FROM AspNetUserLogins;
DELETE FROM AspNetUsers;
DELETE FROM AspNetUserTokens;
DELETE FROM PersistedGrants;
DELETE FROM DeviceCodes;
DELETE FROM AspNetUserRoles;