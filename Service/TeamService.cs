using EmployeeManagement.Models;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Service
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync() =>
            await _teamRepository.GetAllAsync();

        public async Task<Team> GetTeamByIdAsync(int id) =>
            await _teamRepository.GetByIdAsync(id);

        public async Task AddTeamAsync(Team team) =>
            await _teamRepository.AddAsync(team);

        public async Task UpdateTeamAsync(Team team) =>
            await _teamRepository.UpdateAsync(team);

        public async Task DeleteTeamAsync(int id) =>
            await _teamRepository.DeleteAsync(id);
    }
}
