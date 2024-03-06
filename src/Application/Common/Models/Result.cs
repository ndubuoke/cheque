using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace ChequeMicroservice.Application.Common.Models
{

	public static class ApplicationLogging
	{
		public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
		public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
	}

	public class Result
	{
		private readonly ILogger _logger;
        public Result()
        {
            
        }
        public Result(ILogger logger)
		{
			_logger = logger;
		}

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
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(entity));
			logger.LogInformation("Request was executed successfully!");
			return new Result(true, "Request was executed successfully!", entity);
		}

		public static Result Success(Type request, string message)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogInformation(message);
			return new Result(true, message);
		}

		public static Result Success(object entity, string message, Type request)
		{
			ILogger logger = new LoggerFactory().CreateLogger(nameof(request));
			logger.LogInformation(message, entity);
			return new Result(true, message, entity);
		}

		public static Result Success(object entity, Type request)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogInformation("Request was executed successfully!", entity);
			return new Result(true, entity);
		}

		public static Result Success<T>(string message)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogInformation(message);
			return new Result(true, message);
		}

		public static Result Success<T>(string message, object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogInformation($"{message} {Environment.NewLine}" +
				$" {JsonConvert.SerializeObject(entity, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore })}");
			return new Result(true, message, entity);
		}


		public static Result Success<T>(object entity, T request)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogInformation("Request was executed successfully!");
			return new Result(true, entity);
		}

		public static Result Success<T>(object entity, T request, string message)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogInformation(message);
			return new Result(true, message);
		}

		public static Result Success<T>(object entity, string message, T request)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogInformation(message);
			return new Result(true, message, entity);
		}

		public static Result Failure(Type request, string error)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogError(error);
			return new Result(false, error);
		}

		public static Result Failure(Type request, string prefixMessage, Exception ex)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogError($"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
			return new Result(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
		}

		public static Result Failure<T>(string error)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogError(error);
			return new Result(false, error);
		}

		public static Result Failure<T>(T request, string error)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogError(error);
			return new Result(false, error);
		}

		public static Result Failure<T>(string prefixMessage, Exception ex)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogError($"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
			return new Result(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
		}

		public static Result Failure<T>(T request, string prefixMessage, Exception ex)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogError($"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
			return new Result(false, $"{prefixMessage} Error: {ex?.Message + Environment.NewLine + ex?.InnerException?.Message}");
		}

		public static Result Failure<T>(T request, object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogError($"Error: {DateTime.Now}");
			return new Result(false, entity);
		}

		public static Result Failure(Type request, object entity)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogError($"Error: {DateTime.Now}");
			return new Result(false, entity);
		}
		public static Result Failure<T>(object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogError($"Error: {DateTime.Now}");
			return new Result(false, entity);
		}

		public static Result Info(Type request, string information)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogInformation(information);
			return new Result(true, information);
		}

		public static Result Info(Type request, object entity)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogInformation("Information!");
			return new Result(true, entity);
		}

		public static Result Info<T>(T request, string message)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogInformation(message, DateTime.Now);
			return new Result(true, message);
		}

		public static Result Info<T>(T request, object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogInformation("Information");
			return new Result(true, entity);
		}

		public static Result Warning(Type request, string message)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogWarning(message);
			return new Result(false, message);
		}

		public static Result Warning(Type request, object entity)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogWarning("Warning!", entity);
			return new Result(false, entity);
		}

		public static Result Warning<T>(string message)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogWarning(message);
			return new Result(false, message);
		}

		public static Result Warning<T>(object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogWarning("Warning!");
			return new Result(false, entity);
		}

		public static Result Warning<T>(T request, string message)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogWarning(message);
			return new Result(false, message);
		}

		public static Result Warning<T>(T request, object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogWarning("Warning!");
			return new Result(false, entity);
		}


		public static Result Critical(Type request, string message)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogCritical(message);
			return new Result(false, message);
		}

		public static Result Critical(Type request, object entity)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogCritical("Warning!", entity);
			return new Result(false, entity);
		}

		public static Result Critical<T>(string message)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogCritical(message);
			return new Result(false, message);
		}

		public static Result Critical<T>(object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogCritical("Warning!");
			return new Result(false, entity);
		}

		public static Result Critical<T>(T request, string message)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogCritical(message);
			return new Result(false, message);
		}

		public static Result Critical<T>(T request, object entity)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogCritical("Critical!");
			return new Result(false, entity);
		}

		public static Result Exception(Type request, Exception ex)
		{
			ILogger logger = ApplicationLogging.LoggerFactory.CreateLogger(nameof(request));
			logger.LogCritical("Exception:", ex);
			return new Result(false, ex.Message, null, ex?.InnerException?.Message);
		}

		public static Result Exception<T>(T request, Exception ex)
		{
			ILogger logger = ApplicationLogging.CreateLogger<T>();
			logger.LogCritical("Exception:", request, ex);
			return new Result(false, ex.Message, default, ex?.InnerException?.Message);
		}
	}

}