This project could be further split into even tinier microservices.
For example, there could be a BookingApiService, ConsultantsApiService,
ConsultantCalendarApiService, and ConsultantScheduleApiService. That
way, each of these microservices could be scaled and deployed
independently. That day may come, but for now, putting all the
relevant services in one API makes sense. At least this API can be
scaled and deployed independently from the main website. We can
revisit this in the future.