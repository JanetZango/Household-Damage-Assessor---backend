(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBDefinitionQuestion = factory());
}(this, (function () {
    var index = {
        name: 'fb-definition-question',
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
        template: `<div class="question" v-if="value.childCode == 'QUESTION'" v-on:dblclick="subSelectResponseItem($event)">
                        <div class="row">
                            <div class="col-md-11 question-heading">
                                {{value.childDescription}} <span v-if="value.properties.isMandatory == true">*</span>
                            </div>
                            <div class="col-md-1 text-center" v-if="readonly == false">
                                <i class="fa fa-pen edititem" :id="'edititem_' + value.formDefinitionItemID" v-on:click="subSelectResponseItem($event)"></i>
                            </div>
                        </div>
                        <div class="row caution" v-if="value.properties.isCaution">
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
                        <div class="row non-list-response" v-if="value.properties != undefined && value.properties.responseTypeAnswers != undefined && value.properties.responseTypeAnswers.length > 0 && value.properties.isList == false">
                            <div class="col-md-6">
                                <button type="button" v-for="answer in value.properties.responseTypeAnswers" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID" class="btn btn-block btn-default">{{answer.description}}</button>
                            </div>
                        </div>
                        <div class="row list-response" v-if="value.properties != undefined && value.properties.responseTypeAnswers != undefined && value.properties.responseTypeAnswers.length > 0 && value.properties.isList == true">
                            <div class="col-md-6">
                                <button type="button" class="btn btn-block btn-default" data-toggle="modal" :data-target="'#basic_answer_modal_' + value.formDefinitionItemID">
                                    Select response
                                </button>
                                <div class="modal fade" :id="'basic_answer_modal_' + value.formDefinitionItemID" tabindex="-1" role="dialog">
                                    <div class="modal-dialog modal-sm" role="document">
                                        <div class="modal-content" style="border-radius: 5px;">
                                            <div class="modal-body">
                                                <div v-if="value.properties.isListMultiSelect == false">
                                                    <div class="btn-group-vertical full-width" role="group">
                                                        <button type="button" v-for="answer in value.properties.responseTypeAnswers" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID" class="btn btn-block btn-default">{{answer.description}}</button>
                                                    </div>
                                                </div>
                                                <div v-if="value.properties.isListMultiSelect == true">
                                                    <template v-for="answer in value.properties.responseTypeAnswers">
                                                        <label class="anglo-checkbox">
                                                            {{answer.description}}
                                                            <input type="checkbox" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID">
                                                            <span class="checkmark"></span>
                                                        </label>
                                                        <hr style="margin:0; padding:0" />
                                                    </template>
                                                </div>
                                            </div>
                                            <div class="modal-footer" v-if="value.properties.isListMultiSelect == true">
                                                <button type="button" class="btn btn-primary btn-block">Done</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>`
    };
    return index;
})));