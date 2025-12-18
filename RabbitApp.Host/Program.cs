using Rabbit.NewTask;
using Rabbit.Worker;
using RabbitApp.Host;
using RabbitApp.Receive;
using RabbitApp.Send;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<ISend, Send>();
//builder.Services.AddSingleton<IReceive, Receive>();

//builder.Services.AddHostedService<SendWorker>();
//builder.Services.AddHostedService<ReceiveWorker>();

// Worker y NewTask
//builder.Services.AddSingleton<IWorker, Worker>();
//builder.Services.AddSingleton<INewTask, NewTask>();

var loginRabbit = ConfigRabbitMQ.ConfigRabbitMQLogin();

var app = builder.Build();


app.UseAuthorization();
app.MapControllers();

app.Run();
