namespace CleannetCodeBot.Infrastructure;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class IgnoreAutoInjectionAttribute : Attribute
{
}