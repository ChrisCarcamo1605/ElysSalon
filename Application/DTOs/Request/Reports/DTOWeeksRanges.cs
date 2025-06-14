﻿namespace Application.DTOs.Request.Reports;

public record DtoWeeksRanges(
    DateTime week1Start,
    DateTime week1End,
    DateTime week2Start,
    DateTime week2End,
    DateTime week3Start,
    DateTime week3End,
    DateTime week4Start,
    DateTime week4End);