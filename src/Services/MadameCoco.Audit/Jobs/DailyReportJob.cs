using System;
using System.Threading.Tasks;
using MadameCoco.Audit.Services;
using Quartz;

namespace MadameCoco.Audit.Jobs;

public class DailyReportJob : IJob
{
    private readonly AuditService _auditService;

    public DailyReportJob(AuditService auditService)
    {
        _auditService = auditService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var logs = await _auditService.GetLogsLast24HoursAsync();
        
        // Simulate sending email
        Console.WriteLine($"Sending email to admin with {logs.Count} logs...");
    }
}
