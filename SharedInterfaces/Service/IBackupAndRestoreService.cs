using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterfaces.Service
{
    public interface IBackupAndRestoreService
    {
        public Task BackupData(string filePath);
        public Task RestoreData(string filePath);
    }
}
