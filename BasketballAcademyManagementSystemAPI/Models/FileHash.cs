using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class FileHash
{
    public int Id { get; set; }

    public string Hash { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string? FileType { get; set; }

    public long? FileSize { get; set; }
}
