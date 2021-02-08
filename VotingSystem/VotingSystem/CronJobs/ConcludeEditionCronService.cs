using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VotingSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using VotingSystem.Models.Data;
using VotingSystem.Extensions;

namespace VotingSystem.CronJobs
{


    public class ConcludeEditionCronService : CronServiceBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ConcludeEditionCronService(
            CronServiceOptions<ConcludeEditionCronService> options,
            ILogger<CronServiceBase> logger,
            IServiceProvider serviceProvider)
                : base(options.CronExpression, logger)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void ExecuteWork()
        {
            using (var scope = _serviceProvider.CreateScope())
            using (var db = scope.ServiceProvider.GetRequiredService<IVotingSystemDb>())
            {
                var activeEdition = db.EditionsRepo.FindActiveEdition();

                if (activeEdition?.EndDate < DateTime.Now)
                {
                    var concludedEdition = activeEdition.Conclude();
                    db.EditionsRepo.Remove(activeEdition);
                    db.EditionsRepo.Add(concludedEdition);
                    db.SaveChanges();

                    var bestScore = activeEdition.Participants.Max(v => v.Votes.Count);
                    var winningProjects = activeEdition.Participants.Where(p => p.Votes.Count == bestScore);
                    
                    var rng = new Random();
                    int randomIndex = rng.Next(winningProjects.Count());
                    
                    var randomizedWinner = winningProjects.ElementAt(randomIndex).Project;
                    randomizedWinner.MarkAsWinnerIn(concludedEdition);

                    var draftsReferencingTheWinner = db.EditionDraftsRepo.GetAllReferencingProject(randomizedWinner.Id);
                    draftsReferencingTheWinner.ForEach(d => d.RemoveProject(randomizedWinner));

                    db.SaveChanges();
                }
            }
        }
    }
}