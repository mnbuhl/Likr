using System;
using System.Collections.Generic;
using Likr.Comments.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace Likr.Comments.Data
{
    public class RavenDbStore : IRavenDbStore
    {
        public DocumentStore Store { get; init; }
        
        private readonly IConfiguration _configuration;
        private readonly ILogger<RavenDbStore> _logger;

        public RavenDbStore(IConfiguration configuration, ILogger<RavenDbStore> logger)
        {
            _configuration = configuration;
            _logger = logger;
            Store = new DocumentStore
            {
                Database = configuration.GetValue<string>("Database:Name"),
                Urls = configuration.GetSection("Database:Urls").Get<string[]>()
            };

            Store.Initialize();
            
            EnsureCreated();
        }

        private void EnsureCreated()
        {
            string databaseName = _configuration.GetValue<string>("Database:Name");
            
            try
            {
                Store.Maintenance.ForDatabase(databaseName)
                    .Send(new GetStatisticsOperation());

                _logger.LogInformation("Comments database running. No need to recreate.");
            }
            catch (DatabaseDoesNotExistException e)
            {
                _logger.LogWarning(e, "Database not created. Creating Comments Database now.");

                Store.Maintenance.Server.Send(
                    new CreateDatabaseOperation(new DatabaseRecord(databaseName)));
                
                _logger.LogInformation("Comments database successfully created.");
            }
        }
    }
}