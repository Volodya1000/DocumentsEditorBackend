namespace DocumentEditor.Core.Models
{
    namespace DocumentEditor.Core.Models
    {
        public class DocumentDTO
        {
            public Guid Id { get; }
            public string Content { get; }
            public string Title { get; }
            public string UserRoleIdDocument { get; }
            public List<UserRoleInfoDTO> OtherUserRoles { get; }

            private DocumentDTO(Guid id, string title, string content, string userRoleIdDocument, List<UserRoleInfoDTO> otherUserRoles)
            {
                Id = id;
                Title = title;
                Content = content;
                UserRoleIdDocument = userRoleIdDocument;
                OtherUserRoles = otherUserRoles;
            }

            public static DocumentDTO Create(Guid id, string title, string content, string userRoleName, List<UserRoleInfoDTO> otherUserRoles)
            {
                return new DocumentDTO(id, title, content, userRoleName, otherUserRoles);
            }
        }

    }

}
