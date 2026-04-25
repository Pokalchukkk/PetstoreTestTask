using System.Net;

namespace PetstoreTestTask.Models;

public sealed record ApiResponse<T>(
    HttpStatusCode StatusCode,
    T? Body,
    string RawContent
);
