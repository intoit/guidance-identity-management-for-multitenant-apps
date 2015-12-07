﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Tailspin.Surveys.Data.DataModels;

namespace Tailspin.Surveys.Data.DataStore
{
    public class SqlServerContributorRequestStore : IContributorRequestStore
    {
        private ApplicationDbContext dbContext { get; set; }

        public SqlServerContributorRequestStore(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<ContributorRequest>> GetRequestForSurveyAsync(int surveyId)
        {
            return await dbContext.ContributorRequests.Where(r => r.SurveyId == surveyId)
                    .ToArrayAsync();
        }

        public async Task AddRequestAsync(ContributorRequest contributorRequest)
        {
            dbContext.ContributorRequests.Add(contributorRequest);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ContributorRequest>> GetRequestsForUserAsync(string emailAddress)
        {
            //TODO: check if this is optimal
            return await dbContext.ContributorRequests.Where(c => Equals(c.EmailAddress.ToLowerInvariant(), emailAddress.ToLowerInvariant()))
                    .ToArrayAsync();
        }

        public async Task RemoveRequestAsync(ContributorRequest contributorRequest)
        {
            dbContext.ContributorRequests.Remove(contributorRequest);
            await dbContext.SaveChangesAsync();
        }
    }
}
