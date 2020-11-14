import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

    state = {
        shopTraffics: [],
        optionRadio: "option1",
      loading: true
  };

  constructor(props) {
    super(props);
      this.populateWeatherData(1);
      this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        this.setState({
          optionRadio: event.target.value
        });
    }

  static renderForecastsTable(shopTraffics) {
        return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Shop name</th>
            <th>People in</th>
            <th>People out</th>
            <th>People acctual</th>
          </tr>
        </thead>
        <tbody>
                {shopTraffics.map(shopTraffic =>
              <tr key={shopTraffic.id}>
                  <td>{shopTraffic.date}</td>
                        <td>{shopTraffic.shop_id}</td>
                        <td>{shopTraffic.people_entered}</td>
                        <td>{shopTraffic.people_left}</td>
                        <td>{shopTraffic.current_people_quantity}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : FetchData.renderForecastsTable(this.state.shopTraffics);

    return (
      <div>
        <h1 id="tabelLabel" >Shop infromation</h1>
            <p>Strona ta zwraca dane zebrane z czujnikow z poszczegolnego okresu czasu</p> 

            <div className="form-check">
                <input className="form-check-input" type="radio" name="option1" id="exampleRadios1" onChange={this.handleChange} value="option1" checked={this.state.optionRadio === "option1"}/>
                <label className="form-check-label" for="exampleRadios1">
                    Shop 1
                </label>
            </div>
            <div className="form-check">
                <input className="form-check-input" type="radio" name="option2" id="exampleRadios2" onChange={this.handleChange} value="option2" checked={this.state.optionRadio === "option2"}/>
                <label className="form-check-label" for="exampleRadios2">
                        Shop 2
                    </label>
        </div>

                <div class="btn-group" role="group" aria-label="Basic example">
                    <button type="button" onClick={() => this.populateWeatherData(1)}className="btn btn-secondary">Last minute</button>
                    <button type="button" onClick={() => this.populateWeatherData(2)} className="btn btn-secondary">Last hours</button>
                    <button type="button" onClick={() => this.populateWeatherData(3)} className="btn btn-secondary">Last 4 hours</button>
                    <button type="button" onClick={() => this.populateWeatherData(4)} className="btn btn-secondary">Last day</button>
                        <button type="button" onClick={() => this.populateWeatherData(5)} className="btn btn-secondary">Last week</button>
                </div>
        {contents}
        </div>
    );
  }

    async populateWeatherData(optionValue)
    {
        console.log(this.state.optionRadio)
        const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ option: optionValue, optionShop: this.state.optionRadio})
    };
        const response = await fetch('shoptraffic/all', requestOptions);
        const data = await response.json();
        console.log(data);
      this.setState({ shopTraffics: data, loading: false });
  }
}
