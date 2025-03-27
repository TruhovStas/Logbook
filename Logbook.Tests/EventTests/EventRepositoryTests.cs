//using EventsWeb.DataAccess;
//using EventsWeb.DataAccess.Repositories.Impl;
//using EventsWeb.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Xunit;

//public class BaseRepositoryTests
//{
//    private DatabaseContext _context;
//    private EventRepository _repository;

//    public BaseRepositoryTests()
//    {
//        var options = new DbContextOptionsBuilder<DatabaseContext>()
//            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//            .Options;

//        _context = new DatabaseContext(options);
//        _repository = new EventRepository(_context);
//    }

//    [Fact]
//    public async Task AddAsync_ShouldAddEvent()
//    {
//        // Arrange
//        var newEvent = new Event
//        {
//            Title = "Sample Event",
//            Description = "Event Description",
//            DateTime = DateTime.Now,
//            Location = "Sample Location",
//            Category = "Category A",
//            MaxParticipants = 50
//        };

//        // Act
//        var addedEvent = await _repository.AddAsync(newEvent);
//        _context.SaveChanges();

//        // Assert
//        Assert.NotNull(addedEvent);
//        Assert.Equal("Sample Event", addedEvent.Title);
//        Assert.Equal("Event Description", addedEvent.Description);
//        Assert.Equal("Sample Location", addedEvent.Location);
//    }

//    [Fact]
//    public async Task GetAllAsync_ShouldReturnAllEvents()
//    {
//        // Arrange
//        await _repository.AddAsync(new Event { Title = "Event 1", Description = "Desc 1", DateTime = DateTime.Now, Location = "Location 1", MaxParticipants = 20 });
//        await _repository.AddAsync(new Event { Title = "Event 2", Description = "Desc 2", DateTime = DateTime.Now, Location = "Location 2", MaxParticipants = 30 });
//        _context.SaveChanges();

//        // Act
//        var events = await _repository.GetAllAsync();

//        // Assert
//        Assert.Equal(2, events.Count());
//    }

//    [Fact]
//    public async Task GetByIdAsync_ShouldReturnEventById()
//    {
//        // Arrange
//        var newEvent = new Event { Id = 1, Title = "Event by ID", Description = "Desc ID", DateTime = DateTime.Now, Location = "Location ID", MaxParticipants = 10 };
//        await _repository.AddAsync(newEvent);
//        _context.SaveChanges();

//        // Act
//        var eventById = await _repository.GetByIdAsync(1, CancellationToken.None);

//        // Assert
//        Assert.NotNull(eventById);
//        Assert.Equal("Event by ID", eventById.Title);
//        Assert.Equal("Desc ID", eventById.Description);
//    }

//    [Fact]
//    public async Task GetByPageAsync_ShouldReturnPagedEvents()
//    {
//        // Arrange
//        for (int i = 1; i <= 10; i++)
//        {
//            await _repository.AddAsync(new Event { Title = $"Event {i}", Description = $"Desc {i}", DateTime = DateTime.Now, Location = $"Location {i}", MaxParticipants = i * 10 });
//        }
//        _context.SaveChanges();

//        // Act
//        var pagedEvents = await _repository.GetByPageAsync(1, 5);

//        // Assert
//        Assert.Equal(5, pagedEvents.Count());
//        Assert.Equal("Event 1", pagedEvents.First().Title);
//    }

//    [Fact]
//    public void Update_ShouldUpdateEvent()
//    {
//        // Arrange
//        var existingEvent = new Event { Id = 1, Title = "Old Title", Description = "Old Description", DateTime = DateTime.Now, Location = "Old Location", MaxParticipants = 20 };
//        _repository.AddAsync(existingEvent).Wait();
//        _context.SaveChanges();

//        // Act
//        existingEvent.Title = "Updated Title";
//        existingEvent.Description = "Updated Description";
//        _repository.Update(existingEvent);
//        _context.SaveChanges();

//        // Assert
//        var updatedEvent = _context.Set<Event>().FirstOrDefault(e => e.Id == 1);
//        Assert.Equal("Updated Title", updatedEvent.Title);
//        Assert.Equal("Updated Description", updatedEvent.Description);
//    }

//    [Fact]
//    public void Delete_ShouldRemoveEvent()
//    {
//        // Arrange
//        var eventToDelete = new Event { Id = 1, Title = "To Delete", Description = "Desc", DateTime = DateTime.Now, Location = "Delete Location", MaxParticipants = 10 };
//        _repository.AddAsync(eventToDelete).Wait();
//        _context.SaveChanges();

//        // Act
//        _repository.Delete(eventToDelete);
//        _context.SaveChanges();

//        // Assert
//        var deletedEvent = _context.Set<Event>().FirstOrDefault(e => e.Id == 1);
//        Assert.Null(deletedEvent);
//    }
//}