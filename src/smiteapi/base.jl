import HTTP

function send(url::String)
    try
        HTTP.request("GET", url)
    catch e
        error("Response status error: $(e.Response.Status)")
    end
end

function logsend(method::Method.MethodType, url)
    @info "Sending $(method.name) request..." url
end

function logrecv(method::Method.MethodType, res)
    @info "Received $(method.name) response" res.status bodylen=length(res.body)
end

function logjson(resdata)
    @info "Parsed JSON" resdata
end
