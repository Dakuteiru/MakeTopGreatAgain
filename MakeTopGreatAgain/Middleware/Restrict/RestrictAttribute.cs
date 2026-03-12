using System.Net;

namespace MakeTopGreatAgain.Middleware.Restrict;

[AttributeUsage(AttributeTargets.Class |AttributeTargets.Method)]
public class RestrictAttribute(params string[] allowed): Attribute

{
    public IPAddress[] Allowed { get; private set; } = allowed.Select(IPAddress.Parse).ToArray();

}