﻿using System;
using System.Collections.Generic;
using System.Text;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Infrastructure.Exceptions;
using Nancy;
using Newtonsoft.Json;

namespace Maplink.DesafioDev.WebApi.Lib.Handlers
{
    public class ErrorHandler
    {
        private static readonly JsonSerializerSettings CustomSerializerSettings = new CustomSerializerSettings();

        private readonly IDictionary<Type, Func<Exception, dynamic>> _mappedExceptions = new Dictionary<Type, Func<Exception, dynamic>>
        {
            {
                typeof(RequestValidationException), exception => new
                {
                    Messages = ((RequestValidationException)exception).GetErrors(),
                    HttpStatusCode = HttpStatusCode.UnprocessableEntity
                }
            }
        };

        public virtual Response OnError(NancyContext nancyContext, Exception exception)
        {
            var typeException = exception.GetType();

            if (!_mappedExceptions.ContainsKey(typeException))
            {
                return CreateResponse(exception.Message, HttpStatusCode.InternalServerError);
            }

            var mappedException = _mappedExceptions[typeException](exception);
            return CreateResponse(mappedException.Messages, mappedException.HttpStatusCode);
        }

        private static Response CreateResponse(string error, HttpStatusCode httpStatusCode)
        {
            return CreateResponse(new[] { error }, httpStatusCode);
        }

        private static Response CreateResponse(IEnumerable<string> errors, HttpStatusCode httpStatusCode)
        {
            var routeResponse = new RouteResponse(errors);
            var jsonContent = JsonConvert.SerializeObject(routeResponse, CustomSerializerSettings);
            var jsonArray = Encoding.UTF8.GetBytes(jsonContent);

            return new Response
            {
                StatusCode = httpStatusCode,
                ContentType = "application/json",
                Contents = stream => stream.Write(jsonArray, 0, jsonArray.Length)
            };
        }
    }
}