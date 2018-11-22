﻿using UnityEngine;
using XUUI;

public class MoreComplex : MonoBehaviour
{
    ViewModel vm = null;

    void Start()
    {
        vm = new ViewModel(@"
            local observeable = require 'observeable'

            return {
                data = {
                    name = 'john',
                    select = 0,
                    options = {'vegetables', 'meat'},
                },
                computed = {
                    message = function(data)
                        return 'Hello ' .. data.name .. ', your choice is ' .. tostring(data.options[data.select + 1])
                    end
                },
                methods = {
                    add_option = function(data)
                        local tmp = observeable.raw(data.options) -- 只有获取raw后，table.insert之类的函数才能正常操作
                        table.insert(tmp,'Option #' .. (#tmp + 1))
                        data.options = tmp
                    end,
                    static_csharp_callback = CS.SomeClass.Foo,
                },
            }
        ");

        vm.AddEventHandler("instance_csharp_callback", new SomeClass(1024), "Bar");

        vm.Attach(gameObject);
    }

    void OnDestroy()
    {
        vm.Dispose();
    }
}