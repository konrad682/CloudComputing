import React, { Component } from 'react';
import {
    LineChart,
    CartesianGrid,
    XAxis,
    YAxis,
    Tooltip,
    Legend,
    Line,
    ComposedChart,
    Area,
    Bar
} from "recharts";

export class Counter extends Component {
    state = {
        startDate: '',
        endDate: '',
        shopTraffics: [],
        optionRadio: "option1"
    };
 
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);
  }

    handleChange(event) {
        const name = event.target.name;
        const value = event.target.value;
        this.setState({ [name]: value });
    }

    async handleSubmit() {
        console.log(this.state.optionRadio)
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ startDate: this.state.startDate, endDate: this.state.endDate, optionShop: this.state.optionRadio })
        };
        const response = await fetch('shoptraffic/chart', requestOptions);
        const data = await response.json();
        this.setState({ ["shopTraffics"]: data });
    }

  render() {
    return (
      <div>
            <h1>Counter</h1> 

        <div className="form-check">
        <input className="form-check-input" type="radio" name="optionRadio" id="exampleRadios1" onChange={this.handleChange} value="option1" checked={this.state.optionRadio === "option1"} />
        <label className="form-check-label" for="exampleRadios1">
        Shop 1
        </label>
        </div>
        <div className="form-check">
                <input className="form-check-input" type="radio" name="optionRadio" id="exampleRadios2" onChange={this.handleChange} value="option2" checked={this.state.optionRadio === "option2"} />
        <label className="form-check-label" for="exampleRadios2">
        Shop 2
        </label>
        </div>

        <div class="form-group row">
  <label for="example-datetime-local-input" class="col-2 col-form-label">Start date</label>
    <div class="col-10">
                    <input class="form-control" name="startDate" value={this.state.startDate} onChange={this.handleChange} type="datetime-local" id="example-datetime-local-input"/>
                </div>
        <label for="example-datetime-local-input" class="col-2 col-form-label">end date</label>
        <div class="col-10">
                    <input class="form-control" name="endDate" value={this.state.endDate} onChange={this.handleChange} type="datetime-local" id="example-datetime-local-input" />
            </div>
            </div> 
            <button type="button" onClick={() => this.handleSubmit()} className="btn btn-secondary">Szukaj</button>

  <LineChart
  width={700}
  height={500}
  data={this.state.shopTraffics}
  margin={{
      top: 5, right: 30, left: 20, bottom: 5,
  }}
>
<CartesianGrid strokeDasharray="3 3" />
    <XAxis dataKey="name" />
    <YAxis />
    <Tooltip />
    <Legend />
    <Line type="monotone" dataKey="avgPeople" stroke="#8884d8" activeDot={{ r: 8 }} />
</LineChart>


        </div> 
  );
  }
}
