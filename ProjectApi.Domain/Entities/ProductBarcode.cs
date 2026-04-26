using ProjectApi.Domain.Common;

namespace ProjectApi.Domain.Entities;

/// <summary>
/// IT 06: Barcode Product System
/// Code format: XXXX-XXXX-XXXX-XXXX (16 alphanumeric uppercase chars)
/// Barcode standard: Code 39
/// </summary>
public class ProductBarcode : BaseEntity
{
    public string Code { get; set; } = string.Empty;
}
