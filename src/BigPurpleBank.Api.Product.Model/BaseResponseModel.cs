﻿namespace BigPurpleBank.Api.Product.Model;

public class BaseResponseModel<T>
{
    public T Data { get; set; }

    public LinksPaginated Links { get; set; }
    public MetaPaginated Meta { get; set; }
}