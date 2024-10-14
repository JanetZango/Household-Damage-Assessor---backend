(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBDefinitionNumber = factory());
}(this, (function () {
    var index = {
        name: 'fb-definition-number',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'readonly'],
        data: function () {
            return {
                
            };
        },
        computed: {

        },
        watch: {

        },
        created: function () {

        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            subSelectResponseItem(event) {
                this.$emit('select-item', this.value, event, this.iindex, this.cindex, this.sindex, this.iiindex);
            }
        },
        template: `<div class="number" v-if="value.childCode == 'NUMBER'" v-on:dblclick="subSelectResponseItem($event)">
                        <div class="row">
                            <div class="col-md-11 question-heading">
                                {{value.childDescription}} <span v-if="value.properties.isMandatory == true">*</span>
                            </div>
                            <div class="col-md-1 text-center" v-if="readonly == false">
                                <i class="fa fa-pen edititem" :id="'edititem_' + value.formDefinitionItemID" v-on:click="subSelectResponseItem($event)"></i>
                            </div>
                        </div>
                        <div class="row caution mar-btm" v-if="value.properties.isCaution">
                            <div class="col-md-12">
                                <div v-if="value.properties.cautionDescription != null && value.properties.cautionDescription != ''" v-bind:style="[{'background-color': value.properties.cautionColour}, {'float': 'left'}, {'height': '12px'}, {'width': '12px'}, {'border-radius': '3px'}, {'margin-top': '2px'}, {'margin-right': '8px'}, {'display': 'inline-block'}]"></div>
                                <div v-if="value.properties.cautionDescription != null && value.properties.cautionDescription != ''" style="display:inline-block;float:left;margin-right:8px">{{value.properties.cautionDescription}}</div>
                                <div v-if="value.properties.cautionDescription2 != null && value.properties.cautionDescription2 != ''" v-bind:style="[{'background-color': value.properties.cautionColour2}, {'float': 'left'}, {'height': '12px'}, {'width': '12px'}, {'border-radius': '3px'}, {'margin-top': '2px'}, {'margin-right': '8px'}, {'display': 'inline-block'}]"></div>
                                <div v-if="value.properties.cautionDescription2 != null && value.properties.cautionDescription2 != ''" style="display:inline-block;float:left;margin-right:8px">{{value.properties.cautionDescription2}}</div>
                            </div>
                        </div>
                        <div class="row instructions" v-if="value.properties.isInstruction == true">
                            <div class="col-md-12 pad-btm">
                                <span class="instruction" role="button" data-toggle="collapse" :href="'#question_instruction_' + value.formDefinitionItemID" aria-expanded="true"><i class="fa fa-eye"></i> Toggle instructions</span>
                                <div class="collapse in" :id="'question_instruction_' + value.formDefinitionItemID">
                                    <div v-html="value.properties.instructionText"></div>
                                    <img :src="value.properties.instructionImageBase64" v-if="value.properties.instructionImageBase64 != ''" class="img-responsive" />
                                </div>
                            </div>
                        </div>
                        <div class="row mar-btm">
                            <div class="col-md-6">
                                <input type="number" class="form-control" v-model="value.properties.inputValue" @input="update('properties.inputValue', $event.target.value)" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-control-static">{{value.properties.unitOfMeasure}}</label>
                            </div>
                        </div>
                        <div class="row mar-btm" v-if="value.properties.isTolerance == true && value.properties.toleranceRangeFriendlyMessage != null && value.properties.toleranceRangeFriendlyMessage != ''">
                            <div class="col-md-12">
                                <span>{{value.properties.toleranceRangeFriendlyMessage}}</span>
                            </div>
                        </div>
                        <div class="row mar-btm" v-if="value.properties.isTolerance == true && value.properties.toleranceExceededPrompt != null && value.properties.toleranceExceededPrompt != ''">
                            <div class="col-md-12 text-danger">
                                <span v-if="value.properties.tolleranceComparisonOperator == '<' && (value.properties.inputValue > value.properties.toleranceRangeFrom)">{{value.properties.toleranceExceededPrompt}}</span>
                                <span v-if="value.properties.tolleranceComparisonOperator == '>' && (value.properties.inputValue < value.properties.toleranceRangeFrom)">{{value.properties.toleranceExceededPrompt}}</span>
                                <span v-if="value.properties.tolleranceComparisonOperator == '=' && (value.properties.inputValue != value.properties.toleranceRangeFrom)">{{value.properties.toleranceExceededPrompt}}</span>
                                <span v-if="value.properties.tolleranceComparisonOperator == '<=>' && (value.properties.inputValue < value.properties.toleranceRangeFrom || value.properties.inputValue > value.properties.toleranceRangeTo)">{{value.properties.toleranceExceededPrompt}}</span>
                            </div>
                        </div>
                        <div class="row mar-btm" v-if="value.properties.isTolerance == true && (value.properties.toleranceExceededPrompt == null || value.properties.toleranceExceededPrompt == '')">
                            <div class="col-md-12 text-danger">
                                <span v-if="value.properties.tolleranceComparisonOperator == '<' && (value.properties.inputValue > value.properties.toleranceRangeFrom)">Value entered is outside of the tolerance range</span>
                                <span v-if="value.properties.tolleranceComparisonOperator == '>' && (value.properties.inputValue < value.properties.toleranceRangeFrom)">Value entered is outside of the tolerance range</span>
                                <span v-if="value.properties.tolleranceComparisonOperator == '=' && (value.properties.inputValue != value.properties.toleranceRangeFrom)">Value entered is outside of the tolerance range</span>
                                <span v-if="value.properties.tolleranceComparisonOperator == '<=>' && (value.properties.inputValue < value.properties.toleranceRangeFrom || value.properties.inputValue > value.properties.toleranceRangeTo)">Value entered is outside of the tolerance range</span>
                            </div>
                        </div>
                        <div class="row" v-if="value.properties.displayPreviousMeasurement">
                            <div class="col-md-12">
                                <span>Last reading: no previous reading available</span>
                            </div>
                        </div>
                    </div>`
    };
    return index;
})));