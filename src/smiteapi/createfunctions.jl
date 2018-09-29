function createfunction_base(method::Method.MethodType)
    (endpoint::Endpoint.EndpointType=Endpoint.PC; verbose=0, logresjson=false) -> begin
        url = createurl(method, endpoint)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

function createfunction(method::Method.MethodType)
    (session::Session; verbose=0, logresjson=false) -> begin
        url = createurl(method, session)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

function createfunction(method::Method.MethodType, pathfn)
    (session::Session, params...; verbose=0, logresjson=false) -> begin
        url = createurl(method, session, pathfn(params...))
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

function createfunction_fixed(method::Method.MethodType, createurl_args...)
    (session::Session; verbose=0, logresjson=false) -> begin
        url = createurl(method, session, createurl_args...)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

# function createfunction(method::Method.MethodType, args...)
#     (session::Session, args...; verbose=0, logresjson=false) -> begin
#         url = createurl(method, session, args...)
#         send(url)
#     end
# end

function createfunction_lang(method::Method.MethodType)
    # createfunction(method, (lang::Language.LangType) -> "/$(lang.code)")
    (session::Session, lang::Language.LangType; verbose=0, logresjson=false) -> begin
        url = createurl(method, session, lang)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end
