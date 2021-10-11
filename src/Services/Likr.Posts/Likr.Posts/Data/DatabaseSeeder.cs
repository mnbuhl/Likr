using System;
using System.Collections.Generic;
using System.Linq;
using Likr.Posts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Data
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;
        private static Random _random = new Random();
        private Random _randomForWordGeneration = new Random(5);
        private Random _randomForComments = new Random(5);

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();
            _context.Database.Migrate();
            
            if (!_context.Posts.Any() && !_context.Comments.Any())
            {
                List<Post> posts;
                List<Comment> comments;
                var users = new List<Guid>
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid()
                };

                var postIds = new List<string>
                {
                    "22476115-C5AB-4428-8225-7F0440C589BB",
                    "2A15730D-3CB0-4541-8E48-EB4750EBC628",
                    "8164D69D-475A-40B9-97DB-7FCBD2B02DC6",
                    "260739FC-EAB0-49CD-9330-ACE484B703E1",
                    "D1ED8A23-5EA6-425B-9DEE-5E56A0BE4592",
                    "0EC7153C-8235-44C5-8D7C-B7424B154FC3",
                    "3D171AFE-C227-459E-B8BE-1541D84571AA",
                    "3E22D823-8A07-4979-ADB3-AD0B42E05C70",
                    "3F6A33E9-B7A6-4B99-8CA5-6EE38E491F56",
                    "D90CF823-964A-44CA-97F9-CF07887BDE84",
                    "FE8AC82A-9D0D-470A-9175-ED103AD5BB8F",
                    "6364C994-D52E-48F4-96C6-B6C0046DD87D",
                    "596B413B-49E7-4141-B030-C24C7E4477A6",
                    "9FF436E5-AE67-4DDD-B196-4EC474273603",
                    "F187A7D2-EFDE-4F7C-A753-83B0B2FA2166",
                    "95F9530D-F457-42B2-B5C7-EC9317A44181",
                    "3F379F3B-FF10-498E-8B11-1E29EC9B582A",
                    "6262D110-E599-4BBC-944A-7AF9BA3A8E52",
                    "7F6F6F3B-B90E-46CF-969A-7FD64146A68E",
                    "FA18449B-D1E2-461E-9A18-CEFE83FA226A"
                };

                string body =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Donec tincidunt, lectus nec dapibus tempus, nisl arcu facilisis massa, " +
                    "finibus iaculis dolor ligula vel augue. Nulla et condimentum purus, at congue ante. " +
                    "Sed fermentum purus eget tincidunt eleifend. Aliquam consequat finibus mauris non tristique. " +
                    "Morbi elit arcu, varius ac interdum varius, gravida sed urna. Etiam gravida quam sit amet mi " +
                    "sagittis tincidunt. Morbi sed aliquet ante. Donec dui urna, finibus vel elementum id, " +
                    "ullamcorper in ante. Sed sodales ante sed erat tincidunt tincidunt. Phasellus tellus ligula, " +
                    "sollicitudin eu ante sit amet, vulputate gravida dolor. Proin vitae diam volutpat, consectetur elit vel.";

                posts = new List<Post>();

                foreach (string id in postIds)
                {
                    posts.Add(new Post
                    {
                        Id = id,
                        Body = $"{body[new Range(0, _randomForWordGeneration.Next(body.Length))]}",
                        LikesCount = _random.Next(0, 1000),
                        UserId = users[_random.Next(4)].ToString()
                    });
                }

                _context.Posts.AddRange(posts);
                _context.SaveChanges();


                var commentIds = new List<string>
                {
                    "4CDA3F89-0EDD-4D0B-97DF-A3CBEA10DA44",
                    "A91993F4-1490-41E6-B7B1-6B11BC5F3E3E",
                    "78D308B1-1BCD-43E7-ACEA-32CE2C60DE30",
                    "1414D988-28D1-4D79-BF95-7444080C5E0D",
                    "45BEAB89-4751-42BB-A2DB-876E2FC77ECE",
                    "9C08F61C-59B7-4930-AF9C-1B231A0E7E81",
                    "EFCE7AE8-F954-432A-970E-9C58ABECCF04",
                    "89D02AE9-F649-452C-8BF0-E2ACA9670510",
                    "E7707704-E3BA-4AE1-8F5F-6555D6D325EA",
                    "EA64B8E1-672F-4893-A65B-A540EC0AF9D0",
                    "95023595-D09A-468E-A0B3-526A50FF3DB1",
                    "468DB7D4-ED25-462B-A1E1-0D28D4F790EA",
                    "62F970E0-5C2B-4F41-A450-9350E4119A90",
                    "FA734A40-9E86-4515-BBB7-EC218B6D13BF",
                    "958B0FCE-E8DD-405D-9B40-846AB3759DD7",
                    "F5B0C476-23A2-4B95-ADB1-2DEE46E8EA71",
                    "922DD51F-A79E-4FC4-BB17-60C8F4C3021C",
                    "555CE19C-AB73-471F-B517-40438FBCF83B",
                    "841928F0-02D5-42AF-9873-BEC5D9DAAF57",
                    "583C1E93-E0C0-46F4-9BCD-968370C04C33",
                    "943086EF-CB1F-4AB5-AF09-45F4FE9015EC",
                    "D6483190-913E-43EF-B0A9-997F9C2FC4B0",
                    "2FC260EA-F130-4784-995D-8AB1F7817FBC",
                    "56B62FA6-6409-43AA-AAD3-04458B11A73F",
                    "B5D939EE-B516-4B31-B19D-BBA9F915E0D2",
                    "EECF34C6-83A2-48F3-B9DE-C94399B51A29",
                    "EDA80FDB-C7F9-462C-97B3-53545B065CE7",
                    "1AAE04C6-EE5E-48AD-B8DB-14F80EAFE570",
                    "57010DE0-AC3A-419D-A0BE-C52D40393879",
                    "8272E343-9D20-4D5A-9F23-BE9B6627AA45",
                    "87E682C0-72DF-423F-856F-AD9AEB411E0E",
                    "854274E6-89C3-410D-8CF0-A666DD60504B",
                    "0DBD0CB6-F77D-4C2E-8F96-0A91B9CDE2DC",
                    "7A3C144D-B54B-466B-B301-3C02A137A291",
                    "B8F72796-111B-4E21-A653-336A00808515",
                    "774A5EBE-E646-4306-AAAD-8E0950EE6C07",
                    "847684F9-50A4-4279-867C-ED5C323FBB2F",
                    "28320BF6-232C-4F88-8383-0407892E18F5",
                    "33AA411B-7AE7-4D02-A904-DB8AB7A6516A",
                    "7E4856D6-C635-4349-9FB3-BF8D78B7744A",
                    "4D24105D-E9B2-474D-A30A-2DCF1E0D4886",
                    "66CC6989-38E5-46A0-A728-2357CBF2DC88",
                    "00CD2FFA-670C-449F-BD01-BE1C8C2E929D",
                    "6915960E-A1F0-4F8F-98A9-D0D0BEBE5F06",
                    "D1A01BC7-6AD9-458B-84FD-59EF79A5A4A2",
                    "4B35B348-67E2-40AD-85FC-05503574A60E",
                    "22E46C12-AD72-4926-9A20-F5BF211EF8EC",
                    "8369094E-6694-4AED-8ACE-06E1FBBD7863",
                    "69B7AC70-ABB2-418F-9F30-2EE67CC07C9C",
                    "89930EF5-A856-4E39-B519-538295D09FF3"
                };

                comments = new List<Comment>();

                postIds.AddRange(commentIds.GetRange(0, 10));

                foreach (string id in commentIds)
                {
                    comments.Add(new Comment
                    {
                        Id = id,
                        Body = $"{body[new Range(0, _randomForWordGeneration.Next(body.Length))]}",
                        LikesCount = _random.Next(0, 1000),
                        UserId = users[_random.Next(4)].ToString(),
                        PostId = postIds[_randomForComments.Next(postIds.Count)]
                    });
                }
                
                _context.Comments.AddRange(comments);

                _context.SaveChanges();
                
                var commentsWithoutCommentCount = _context.Comments.ToList();
                var postsWithoutCommentsCount = _context.Posts.ToList();

                foreach (var comment in commentsWithoutCommentCount)
                {
                    var commentsCount = _context.Comments.Count(x => x.PostId == comment.PostId);

                    var postsToUpdate = postsWithoutCommentsCount.Where(x => x.Id == comment.PostId).ToList();

                    if (postsToUpdate.Any())
                    {
                        postsToUpdate.ForEach(x =>
                        {
                            x.CommentsCount = commentsCount;
                            _context.Update(x);
                            _context.SaveChanges();
                        });
                    }
                    else
                    {
                        comment.CommentsCount = commentsCount;
                    }
                }
                
                _context.Comments.UpdateRange(commentsWithoutCommentCount);
                _context.SaveChanges();
            }
        }
    }
}