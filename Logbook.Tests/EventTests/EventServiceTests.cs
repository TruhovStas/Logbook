using AutoMapper;
using EventsWeb.BusinessLogic.Exceptions;
using EventsWeb.BusinessLogic.Models.Events;
using EventsWeb.BusinessLogic.Services.Impl;
using EventsWeb.DataAccess;
using EventsWeb.Domain.Entities;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

public class EventServiceTests
{
    private EventService _eventService;
    private Mock<IMapper> _mapperMock;
    private IUnitOfWork _unitOfWork;
    private DatabaseContext _context;

    public EventServiceTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new DatabaseContext(options);
        _mapperMock = new Mock<IMapper>();
        _unitOfWork = new UnitOfWork(_context);
        _eventService = new EventService(_unitOfWork, _mapperMock.Object);
    }

    [Fact]
    public async Task GetEventsAsync_ShouldReturnAllEvents()
    {
        // Arrange
        var dateTime = DateTime.Now;
        _context.Events.AddRange
        (
            new Event { Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20 },
            new Event { Title = "Event 2", Description = "Desc 2", DateTime = dateTime, Location = "Location 2", MaxParticipants = 30 }
        );
        await _context.SaveChangesAsync();

        var expectedEvents = new List<EventResponseDto>
            {
                new EventResponseDto { Id = 1, Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20},
                new EventResponseDto { Id = 1, Title = "Event 2", Description = "Desc 2", DateTime = dateTime, Location = "Location 2", MaxParticipants = 30}
            };
        _mapperMock.Setup(m => m.Map<IEnumerable<EventResponseDto>>(It.IsAny<IEnumerable<Event>>()))
            .Returns(expectedEvents);

        // Act
        var events = await _eventService.GetEventsAsync(CancellationToken.None);

        // Assert
        Assert.Equal(2, events.Count());
    }

    [Fact]
    public async Task GetEventByIdAsync_ShouldReturnEvent_WhenEventExists()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var ev = new Event { Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20 };
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();

        var expectedEvent = new EventResponseDto
        { Id = 1, Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20 };
        _mapperMock.Setup(m => m.Map<EventResponseDto>(ev)).Returns(expectedEvent);

        // Act
        var result = await _eventService.GetEventByIdAsync(1, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreateEventAsync_ShouldCreateEvent()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var eventCreateDto = new EventCreateDto { Id = 1, Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20 };
        var ev = new Event { Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20 };
        var eventCreateResponseDto = new EventCreateResponseDto { Id = 1 };

        _mapperMock.Setup(m => m.Map<Event>(eventCreateDto)).Returns(ev);
        _mapperMock.Setup(m => m.Map<EventCreateResponseDto>(ev)).Returns(eventCreateResponseDto);

        // Act
        var result = await _eventService.CreateEventAsync(eventCreateDto, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(eventCreateResponseDto.Id, result.Id);
    }

    [Fact]
    public void GetEventByIdAsync_ShouldThrowNotFoundException_WhenEventDoesNotExist()
    {
        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _eventService.GetEventByIdAsync(99, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateEventAsync_ShouldUpdateEvent()
    {
        // Arrange
        var dateTime = DateTime.Now;

        var existingEvent = new Event { Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20 };
        _context.Events.Add(existingEvent);
        await _context.SaveChangesAsync();

        var eventUpdateDto = new EventUpdateDto { Id = 1, Title = "New Title", Description = "New Desc", DateTime = dateTime, Location = "New Location", MaxParticipants = 30 };
        _mapperMock.Setup(m => m.Map(eventUpdateDto, existingEvent))
                   .Callback<EventUpdateDto, Event>((src, dest) =>
                   {
                       dest.Title = src.Title;
                       dest.Description = src.Description;
                       dest.DateTime = src.DateTime;
                       dest.Location = src.Location;
                       dest.MaxParticipants = src.MaxParticipants;
                   });


        // Act
        var result = await _eventService.UpdateEventAsync(eventUpdateDto, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Title", _context.Events.First().Title);
        Assert.Equal("New Desc", _context.Events.First().Description);
        Assert.Equal("New Location", _context.Events.First().Location);
        Assert.Equal(30, _context.Events.First().MaxParticipants);
    }

    [Fact]
    public async Task DeleteEventAsync_ShouldDeleteEvent()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var ev = new Event { Title = "Event 1", Description = "Desc 1", DateTime = dateTime, Location = "Location 1", MaxParticipants = 20 };
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();

        // Act
        var result = await _eventService.DeleteEventAsync(1, CancellationToken.None);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Null(await _context.Events.FindAsync(1));
    }
}
