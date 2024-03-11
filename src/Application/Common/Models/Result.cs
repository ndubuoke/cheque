using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Linq;

namespace ChequeMicroservice.Application.Common.Models
{

	public class Result
	{
		internal Result(bool succeeded, string message, object entity = default, string exception = null)
		{
			Succeeded = succeeded;
			Message = message;
			ExceptionError = exception;
			Entity = entity;
		}

		internal Result(bool succeeded, object entity = default)
		{
			Succeeded = succeeded;
			Entity = entity;
		}

		public bool Succeeded { get; set; }
		public object Entity { get; set; }
		public string Error { get; set; }
		public string ExceptionError { get; set; }
		public string Message { get; set; }

		public static Result Success(object entity)
		{
            Log.Information($"Request was executed successfully! {entity} ");
			return new Result(true, "Request was executed successfully!", entity);
		}

		public static Result Success(Type request, string message)
		{
            Log.Information(message);
			return new Result(true, message);
		}

		public static Result Success(object entity, string message, Type request)
		{
            Log.Information(message, entity);
			return new Result(true, message, entity);
		}

		public static Result Success(object entity, Type request)
		{
            Log.Information($"Request was executed successfully!-{entity}",entity);
			return new Result(true, entity);
		}

		public static Result Success(string message)
		{
            Log.Information(message);
			return new Result(true, message);
		}

		public static Result Success(string message, object entity)
		{
            Log.Information($"{message} {Environment.NewLine}" +
				$" {JsonConvert.SerializeObject(entity, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore })}");
			return new Result(true, message, entity);
		}

		public static Result Failure(Type request, string error)
		{
            Log.Error(error);
			return new Result(false, error);
		}

		public static Result Failure(Type request, string prefixMessage, Exception ex)
		{
            Log.Error($"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
			return new Result(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
		}

		public static Result Failure(string error)
		{
            Log.Error(error);
			return new Result(false, error);
		}

		public static Result Failure<T>(T request, string error)
		{
            Log.Error(error);
			return new Result(false, error);
		}

		public static Result Failure(string prefixMessage, Exception ex)
		{
            Log.Error($"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
			return new Result(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
		}

		public static Result Failure<T>(T request, string prefixMessage, Exception ex)
		{
            Log.Error($"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
			return new Result(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
		}

		public static Result Failure<T>(T request, object entity)
		{
			Log.Error($"Error: {DateTime.Now}");
			return new Result(false, entity);
		}

		public static Result Failure(Type request, object entity)
		{
            Log.Error($"Error: {DateTime.Now}");
			return new Result(false, entity);
		}
		public static Result Failure(object entity)
		{
			Log.Error($"Error: {DateTime.Now}");
			return new Result(false, entity);
		}
	}

}