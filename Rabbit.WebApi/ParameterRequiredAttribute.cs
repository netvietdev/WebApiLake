using System;

namespace Rabbit.WebApi
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParameterRequiredAttribute : Attribute
    {
    }
}