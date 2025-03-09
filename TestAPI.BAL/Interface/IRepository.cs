using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI.DAL;

namespace TestAPI.BAL.Interface
{
    public interface IRecordRepository
    {
        Task<IEnumerable<Record>> GetUserAsync();
        Task<int> InsertUserAsync(Record user);
        Task<int> UpdateUserAsync(Record user);
        Task<Record> GetUserByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);

    }
}
