using System.Threading.Tasks;
using Logic.Model;

namespace Logic.Services
{
    public interface IQueueLogic
    {
        Task AddStudent(Student student);
    }
}