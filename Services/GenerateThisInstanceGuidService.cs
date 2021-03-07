using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTasksQueue.Library.Services
{
    // вставить генерацию уникального номера в сервис констант - уже нет, оставить здесь
    // сделать общую библиотеку для всех sln
    public class GenerateThisInstanceGuidService
    {
        private readonly string _thisBackServerGuid;

        public GenerateThisInstanceGuidService()
        {
            _thisBackServerGuid = Guid.NewGuid().ToString();
        }

        public string ThisBackServerGuid()
        {
            return _thisBackServerGuid;
        }
    }
}
