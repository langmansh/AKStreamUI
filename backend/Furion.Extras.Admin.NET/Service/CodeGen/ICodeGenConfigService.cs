using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    public interface ICodeGenConfigService
    {
        Task Add(CodeGenConfig input);

        void AddList(List<TableColumnOuput> tableColumnOuputList, SysCodeGen codeGenerate);

        Task Delete(long codeGenId);

        Task<SysCodeGenConfig> Detail([FromQuery] CodeGenConfig input);

        Task<List<CodeGenConfig>> List([FromQuery] CodeGenConfig input);

        Task Update(List<CodeGenConfig> inputList);
    }
}