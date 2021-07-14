using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service.Notice
{
    public interface ISysNoticeUserService
    {
        Task Add(long noticeId, List<long> noticeUserIdList, NoticeUserStatus noticeUserStatus);

        Task<List<SysNoticeUser>> GetNoticeUserListByNoticeId(long noticeId);

        Task Read(long noticeId, long userId, NoticeUserStatus status);

        Task Update(long noticeId, List<long> noticeUserIdList, NoticeUserStatus noticeUserStatus);
    }
}