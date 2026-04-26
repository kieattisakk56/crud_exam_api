using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 07: QR Code Product System
/// Code format: XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX (30 alphanumeric uppercase chars)
/// Must be unique (no duplicates)
/// </summary>
public class ProductQrCode : BaseEntity
{
    public string Code { get; set; } = string.Empty;
}
